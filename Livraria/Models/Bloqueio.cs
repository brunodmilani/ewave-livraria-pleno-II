using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Models
{
    public class Bloqueio
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public long UsuarioId { get; set; }
        public DateTime DataVencimento { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
