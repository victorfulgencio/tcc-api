using System.Collections.Generic;

namespace tcc_back.Dtos
{
    public class MobileOperator
    {
        public string Name { get; set; }
        public decimal? Rating { get; set; }
    }
    public class FuzzyClassifierOutputDto
    {
        public IEnumerable<MobileOperator> MobileOperators { get; set; }
    }
}