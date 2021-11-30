namespace tcc_back.Models
{
    public class FuzzyInputs
    {
        public decimal city_coverage2G { get; set; }
        public decimal city_coverage3G { get; set; }
        public decimal city_coverage4G { get; set; }
        
        public decimal most_valuable_areas_coverage2G { get; set; }
        public decimal most_valuable_areas_coverage3G { get; set; }
        public decimal most_valuable_areas_coverage4G { get; set; }
        
        public decimal? cost { get; set; }
        public int? service { get; set; }
        public decimal claimed_issues { get; set; }
    }
}