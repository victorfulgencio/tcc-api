using System.Collections.Generic;

namespace tcc_back.Repositories
{
    public interface ICoberturaRepository
    {
        IEnumerable<string> GetCities(string uf);
    }

}