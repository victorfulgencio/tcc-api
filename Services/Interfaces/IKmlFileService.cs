using System.Collections.Generic;

namespace tcc_back.Services
{
    public interface IKmlFileService
    {
        string GetFilePath(string uf, string city);
    }

}