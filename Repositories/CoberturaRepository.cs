using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
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
      const string sql = @"SELECT NOME FROM cidades WHERE UF = (@UF) ORDER BY NOME";
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

    public IEnumerable<CoveragePercentage> GetCityAvgPercentualCobertura(string uf, string city)
    {
      const string sql = @"SELECT  Operadora, Tecnologia, AVG(CONVERT(Percentual_Cobertura, DOUBLE )) Percentual_Cobertura 
                                FROM cobertura WHERE UF = (@UF) AND Municipio = (@City) GROUP BY Operadora, Tecnologia";
      return _dbConnection.Query<CoveragePercentage>(sql, new { UF = uf, City = city });
    }

    public IEnumerable<CoveragePercentage> GetAreasAvgPercentualCobertura(IEnumerable<string> selectedAreas)
    {
      const string sql = @"SELECT  Operadora, Tecnologia, AVG(CONVERT(Percentual_Cobertura, DOUBLE )) Percentual_Cobertura 
                                FROM cobertura WHERE Setor_Censitario in @Areas GROUP BY Operadora, Tecnologia";
      return _dbConnection.Query<CoveragePercentage>(sql, new { Areas = selectedAreas });
    }

    public double GetTotalAccessFor(string uf, string city, string company)
    {
      const string sql = @"SELECT  AcessosTotais FROM acessos_totais_empresa_municipio
                                WHERE UF = (@UF) AND Municipio = (@City) AND Empresa = (@Company)";
      return _dbConnection.Query<double>(sql, new { UF = uf, City = city, Company = company }).FirstOrDefault();
    }

    public double GetTotalClaimedIssuesFor(string uf, string city, string company)
    {
      const string sql = @"SELECT  TotalReclamacoes FROM total_reclamacoes_cidade_marca
                                WHERE UF = (@UF) AND Cidade = (@City) AND Marca = (@Company)";
      return _dbConnection.Query<double>(sql, new { UF = uf, City = city, Company = company }).FirstOrDefault();
    }
  }

}