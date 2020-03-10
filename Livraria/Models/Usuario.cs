using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Models
{
    public class Usuario
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nome { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string Endereco { get; set; }
        [Required]
        [Column(TypeName = "varchar(14)")]
        public string CPF { get; set; }
        public Nullable<long> InstituicaoId { get; set; }
        [Column(TypeName = "varchar(16)")]
        public string Telefone { get; set; }
        public string Email { get; set; }
        public virtual Instituicao Instituicao { get; set; }
    }
}
