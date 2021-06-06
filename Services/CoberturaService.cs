using System.Collections.Generic;
using tcc_back.Repositories;
using tcc_back.Dtos;
using tcc_back.util;
using System.Linq;

namespace tcc_back.Services
{
    public class CoberturaService : ICoberturaService
    {
        public CoberturaService(ICoberturaRepository repository)
        {
            _repository = repository;
        }

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

            var result = coberturaModelList.Select((cobertura) =>
                new AreaDto(cobertura.Setor_Censitario, cobertura.Bairro, cobertura.Tipo_Setor)
            ).ToList();

            Validation.emptyResultList(result);

            return result;
        }
    }

}