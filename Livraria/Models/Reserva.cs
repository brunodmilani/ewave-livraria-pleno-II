using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Models
{
    public class Reserva
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public long UsuarioId { get; set; }
        [Required]
        public long LivroId { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Livro Livro { get; set; }
    }
}
