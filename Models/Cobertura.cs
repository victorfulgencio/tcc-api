using System;
using System.ComponentModel.DataAnnotations;

namespace tcc_back.Models
{
    public class Cobertura
    {
        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Ano { get; set; }

        [Required]
        public string Operadora { get; set; }
        [Required]
        public string Tecnologia { get; set; }
        [Required]
        public string Setor_Censitario { get; set; }
        public string Bairro { get; set; }
        public string Tipo_Setor { get; set; }
        public string Codigo_Localidade { get; set; }
        public string Nome_Localidade { get; set; }
        public string Categoria_Localidade { get; set; }
        public string Localidade_Agregadora { get; set; }

        [Required]
        public string Codigo_Municipio { get; set; }
        public string Municipio { get; set; }

        [Required]
        [StringLength(30)]
        public string UF { get; set; }
        public string Regiao { get; set; }
        public string Domicilios { get; set; }
        public string Moradores { get; set; }

        [Required]
        public string Percentual_Cobertura { get; set; }

    }
}