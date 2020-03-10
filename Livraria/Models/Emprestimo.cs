using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Models
{
    public class Emprestimo
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public long UsuarioId { get; set; }
        [Required]
        public long LivroId { get; set; }
        [Required]
        public DateTime Data { get; set; }
        public DateTime DataDevolucao { get; set; }
        public bool Devolucao { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Livro Livro { get; set; }
    }
}
