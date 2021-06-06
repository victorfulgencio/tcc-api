using System;
using System.Data;
using System.Collections.Generic;
using tcc_back.Models;
using Dapper;

namespace tcc_back.Repositories
{
    public class CoberturaRepository : ICoberturaRepository
    {
        private readonly IDbConnection _dbConnection;

        public CoberturaRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public IEnumerable<string> GetCities(string uf)
        {
            const string sql = @"SELECT DISTINCT MUNICIPIO FROM cobertura WHERE UF = (@UF) ORDER BY MUNICIPIO";
            return _dbConnection.Query<string>(sql, new { UF = uf });
        }

        public string GetCityCode(string uf, string city)
        {
            const string sql = @"SELECT DISTINCT Codigo_Municipio FROM cobertura WHERE UF = (@UF) AND Municipio = (@City) LIMIT 1";
            try
            {
                return _dbConnection.QuerySingle<string>(sql, new { UF = uf, City = city });
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        public IEnumerable<Cobertura> GetAreas(string uf, string city)
        {
            const string sql = @"SELECT DISTINCT Setor_Censitario, Tipo_Setor, Bairro FROM cobertura WHERE UF = (@UF) AND Municipio = (@City)";
            return _dbConnection.Query<Cobertura>(sql, new { UF = uf, City = city });
        }
    }

}