using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Livraria.Models
{
    public class Livro
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Titulo { get; set; }
        [Required]
        public long GeneroId { get; set; }        
        [Required]
        public long AutorId { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string Sinopse { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string CapaPath { get; set; }
        public virtual Autor Autor { get; set; }        
        public virtual Genero Genero { get; set; }
    }
}
