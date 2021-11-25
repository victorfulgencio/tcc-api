using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using tcc_back.Dtos;

namespace tcc_back.Services
{
    public interface ICoberturaService
    {
        IEnumerable<string> GetCitiesFromState(string uf);
        string GetCityCode(string uf, string city);

        IEnumerable<AreaDto> GetAreas(string uf, string city);

        Task<IEnumerable<MobileOperator>>  GetFuzzyClassifierOutput(FuzzyClassifierInputDto inputDto);
    }

}