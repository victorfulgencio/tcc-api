using System.Collections.Generic;
using tcc_back.Repositories;

namespace tcc_back.Services
{
    public class CoberturaService : ICoberturaService
    {
        public CoberturaService(ICoberturaRepository repository)
        {
            _repository = repository;
        }

        private readonly ICoberturaRepository _repository;

        public IEnumerable<string> GetCitiesFromState(string uf) => _repository.GetCities(uf);
    }

}