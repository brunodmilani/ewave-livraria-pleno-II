using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Livraria.Models
{
    public class Genero
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nome { get; set; }
    }
}
