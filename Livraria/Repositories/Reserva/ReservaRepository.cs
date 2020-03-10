using Livraria.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Livraria.Repository
{
    public class ReservaRepository : IReservaRepository
    {
        private readonly LivrariaContext _context;

        public ReservaRepository(LivrariaContext context)
        {
            _context = context;
        }

        public void Add(Reserva reserva)
        {
            _context.Reservas.Add(reserva);
            _context.SaveChanges();
        }

        public Reserva Find(long id)
        {
            return _context.Reservas.Include("Usuario").Include("Livro").AsNoTracking().FirstOrDefault(a => a.Id == id);
        }
        
        public IEnumerable<Reserva> GetAll()
        {
            return _context.Reservas.Include("Usuario").Include("Livro").ToList();
        }

        public void Remove(long id)
        {
            var entity = _context.Reservas.First(u => u.Id == id);
            _context.Reservas.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Reserva reserva)
        {
            _context.Reservas.Update(reserva);
            _context.SaveChanges();
        }

        public bool Exists(long id)
        {
            return _context.Reservas.Any(e => e.Id == id);
        }

        public bool QuantidadeLivrosByUsuario(long usuarioId)
        {
            return _context.Emprestimos.AsNoTracking().Where(a => a.UsuarioId == usuarioId && a.Devolucao == false).Count() >= 2;
        }

        public bool ExisteEmprestimo(long livroId)
        {
            return _context.Emprestimos.AsNoTracking().Any(a => a.LivroId == livroId && a.Devolucao == false);
        }

        public bool UsuarioBloqueado(long usuarioId)
        {
            return _context.Bloqueios.AsNoTracking().Any(a => a.UsuarioId == usuarioId && a.DataVencimento.Date <= DateTime.Now.Date);
        }

        public void Emprestimo(long id)
        {
            Reserva reserva = _context.Reservas.Find(id);
            Emprestimo emprestimo = new Emprestimo();
            emprestimo.LivroId = reserva.LivroId;
            emprestimo.UsuarioId = reserva.UsuarioId;
            emprestimo.Data = DateTime.Now.Date;
            emprestimo.DataDevolucao = DateTime.Now.Date.AddDays(30);
            emprestimo.Devolucao = false;
            _context.Emprestimos.Add(emprestimo);
            _context.Reservas.Remove(reserva);
            _context.SaveChanges();
        }
    }
}
