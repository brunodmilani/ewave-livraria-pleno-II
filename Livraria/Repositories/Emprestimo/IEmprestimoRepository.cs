using Livraria.Models;
using Livraria.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Repository
{
    public interface IEmprestimoRepository
    {
        void Add(Emprestimo emprestimo);
        IEnumerable<EmprestimoVM> GetAll();
        Emprestimo Find(long id);
        void Remove(long id);
        void Update(Emprestimo emprestimo);
        bool Exists(long id);
        bool QuantidadeLivrosByUsuario(long usuarioId);
        bool UsuarioBloqueado(long usuarioId);
        void Devolucao(long id);
    }
}
