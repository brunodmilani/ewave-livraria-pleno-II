using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Livraria.Models
{
    public class Instituicao
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nome { get; set; }
        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Endereco { get; set; }
        [Required]
        [Column(TypeName = "varchar(28)")]
        public string CNPJ { get; set; }
        [Required]
        [Column(TypeName = "varchar(14)")]
        public string Telefone { get; set; }
    }
}
