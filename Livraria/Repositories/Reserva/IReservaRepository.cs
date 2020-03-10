using Livraria.Models;
using System.Collections.Generic;

namespace Livraria.Repository
{
    public interface IReservaRepository
    {
        void Add(Reserva reserva);
        IEnumerable<Reserva> GetAll();
        Reserva Find(long id);
        void Remove(long id);
        void Update(Reserva reserva);
        bool Exists(long id);
        bool QuantidadeLivrosByUsuario(long usuarioId);
        bool ExisteEmprestimo(long livroId);
        bool UsuarioBloqueado(long usuarioId);
        void Emprestimo(long id);
    }
}
