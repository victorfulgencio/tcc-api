using System;
using System.Collections.Generic;
using tcc_back.Repositories;
using tcc_back.Dtos;
using tcc_back.util;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using tcc_back.Models;

namespace tcc_back.Services
{
    public class CoberturaService : ICoberturaService
    {
        public CoberturaService(ICoberturaRepository repository, IConfiguration config)
        {
            _repository = repository;
            _httpClient = new HttpClient();
            _config = config;
        }

        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient; 
        private readonly ICoberturaRepository _repository;

        public IEnumerable<string> GetCitiesFromState(string uf)
        {
            Validation.emptyParameter(uf);
            var result = _repository.GetCities(uf);
            Validation.emptyResultList(result.ToList());

            return result;
        }

        public string GetCityCode(string uf, string city)
        {
            Validation.emptyParameter(uf);
            Validation.emptyParameter(city);
            var result = _repository.GetCityCode(uf, city);
            Validation.emptyResult(result);

            return result;
        }

        public IEnumerable<AreaDto> GetAreas(string uf, string city)
        {
            Validation.emptyParameter(uf);
            Validation.emptyParameter(city);
            var coberturaModelList = _repository.GetAreas(uf, city);

            var result = coberturaModelList.Select(cobertura =>
                new AreaDto(cobertura.Setor_Censitario, cobertura.Bairro, cobertura.Tipo_Setor)
            ).ToList();

            Validation.emptyResultList(result);

            return result;
        }

        private async Task<decimal?> PostForFuzzyOutputAsync(FuzzyInputs input)
        {
            var httpContent = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");
            var fuzzyLambdaUrl = _config.GetValue<string>("FuzzyLambdaUrl");
            var response = await _httpClient.PostAsync(fuzzyLambdaUrl, httpContent);
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<RatingDto>(responseString)?.rating;
        }

        private void SetDefaultValuesIfNull(FuzzyInputs fuzzyParams)
        {
            fuzzyParams.cost ??= 120; // R$120,00
            fuzzyParams.service ??= 30; // 50 GB/month
        }

        private void SetClaimedIssuesPerAccess(FuzzyInputs fuzzyParams, double claimedIssues, double totalAccess)
        {
            try
            {
                fuzzyParams.claimed_issues = Convert.ToDecimal(claimedIssues / totalAccess);
            }
            catch (OverflowException)
            {
                // maybe the total value would be too big/ too low for decimal type
                fuzzyParams.claimed_issues = claimedIssues > totalAccess ? decimal.MaxValue : decimal.MinValue;
            }
        }
        
        public async Task<IEnumerable<MobileOperator>> GetFuzzyClassifierOutputAsync(FuzzyClassifierInputDto inputDto)
        {
            var cityCoverageList = _repository.GetCityAvgPercentualCobertura(inputDto.state, inputDto.city);
            var areasCoverageList = _repository.GetAreasAvgPercentualCobertura(inputDto.selectedAreas);

            var result = new List<MobileOperator>();
            var mobileOperators = cityCoverageList.Select(c => c.Operadora.ToUpper()).Distinct();
            foreach (var mobileOperator in mobileOperators)
            {
                var fuzzyInputsObject = new FuzzyInputs();
                fuzzyInputsObject.city_coverage2G = cityCoverageList.Where(c => c.Operadora == mobileOperator && c.Tecnologia == "2G")
                    .Select(c => c.Percentual_Cobertura).FirstOrDefault();
                fuzzyInputsObject.city_coverage3G  = cityCoverageList.Where(c => c.Operadora == mobileOperator && c.Tecnologia == "3G")
                    .Select(c => c.Percentual_Cobertura).FirstOrDefault();
                fuzzyInputsObject.city_coverage4G  = cityCoverageList.Where(c => c.Operadora == mobileOperator && c.Tecnologia == "4G")
                    .Select(c => c.Percentual_Cobertura).FirstOrDefault();
                
                fuzzyInputsObject.most_valuable_areas_coverage2G = areasCoverageList.Where(c => c.Operadora == mobileOperator && c.Tecnologia == "2G")
                    .Select(c => c.Percentual_Cobertura).FirstOrDefault();
                fuzzyInputsObject.most_valuable_areas_coverage3G = areasCoverageList.Where(c => c.Operadora == mobileOperator && c.Tecnologia == "3G")
                    .Select(c => c.Percentual_Cobertura).FirstOrDefault();
                fuzzyInputsObject.most_valuable_areas_coverage4G = areasCoverageList.Where(c => c.Operadora == mobileOperator && c.Tecnologia == "4G")
                    .Select(c => c.Percentual_Cobertura).FirstOrDefault();

                 (fuzzyInputsObject.cost, fuzzyInputsObject.service) = _repository.GetPlanForMobileOperator(inputDto.state, inputDto.city, mobileOperator);

                var claimedIssues = _repository.GetTotalClaimedIssuesFor(inputDto.state, inputDto.city, mobileOperator);
                var totalAccess = _repository.GetTotalAccessFor(inputDto.state, inputDto.city, mobileOperator);
                SetClaimedIssuesPerAccess(fuzzyInputsObject, claimedIssues, totalAccess);
                SetDefaultValuesIfNull(fuzzyInputsObject);
                    
                result.Add(new MobileOperator()
                {
                    Name = mobileOperator, 
                    Rating = await PostForFuzzyOutputAsync(fuzzyInputsObject)
                });
            }
            
            return result;
        }
    }

}