using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.ViewModels
{
    public class EmprestimoVM
    {
        public long Id { get; set; }
        public long UsuarioId { get; set; }
        public string Usuario { get; set; }
        public long LivroId { get; set; }
        public string Livro { get; set; }
        public DateTime Data { get; set; }
        public DateTime DataDevolucao { get; set; }
        public int DiasRestantes { get; set; }
        public bool Devolucao { get; set; }
    }
}
