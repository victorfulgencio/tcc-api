using System.Collections.Generic;

namespace tcc_back.Dtos
{
    public class FuzzyClassifierInputDto
    {
        public string city { get; set; }
        public string state { get; set; }
        public IEnumerable<string> selectedAreas { get; set; }
    }
}