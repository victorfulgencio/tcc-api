using System.Globalization;
using System.Text;
using System.Collections.Generic;
using tcc_back.Repositories;
using tcc_back.util;
using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace tcc_back.Services
{
  public class KmlFileService : CoberturaService, IKmlFileService
  {

    public KmlFileService(ICoberturaRepository repository, IConfiguration config) : base(repository, config)
    { }

    public string GetFilePath(string uf, string city)
    {
      var cityCode = GetCityCode(uf, city);
      var cityNameWithoutAccents = RemoveAccents(city);
      return $"{cityCode}_{cityNameWithoutAccents}_Setores_2020.geojson";
    }

    private string RemoveAccents(string text)
    {
      StringBuilder sbReturn = new StringBuilder();
      var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
      foreach (char letter in arrayText)
      {
        if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
          sbReturn.Append(letter);
      }
      return sbReturn.ToString();
    }
  }
}