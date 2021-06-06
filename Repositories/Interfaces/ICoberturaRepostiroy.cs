using System.Collections.Generic;
using tcc_back.Models;

namespace tcc_back.Repositories
{
    public interface ICoberturaRepository
    {
        IEnumerable<string> GetCities(string uf);
        string GetCityCode(string uf, string city);
        IEnumerable<Cobertura> GetAreas(string uf, string city);
    }

}