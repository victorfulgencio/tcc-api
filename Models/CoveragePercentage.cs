using System.ComponentModel.DataAnnotations;

namespace tcc_back.Models
{
    public class CoveragePercentage
    {
        [Required]
        public string Operadora { get; set; }
        [Required]
        public string Tecnologia { get; set; }
        [Required]
        public decimal Percentual_Cobertura { get; set; }
    }
}