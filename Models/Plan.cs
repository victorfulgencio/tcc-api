using System.ComponentModel.DataAnnotations;

namespace tcc_back.Models
{
    public class Plan
    {
        public decimal? Price { get; set; }
        
        public int? Gigabytes { get; set; }
    }
}