using System.Collections.Generic;

namespace tcc_back.Services
{
    public interface ICoberturaService
    {
        IEnumerable<string> GetCitiesFromState(string UF);
        string GetCityCode(string uf, string city);
    }

}