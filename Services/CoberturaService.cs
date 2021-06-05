using System.Collections.Generic;
using tcc_back.Repositories;
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
            Validation.emptyList(result.ToList());

            return result;
        }
    }

}