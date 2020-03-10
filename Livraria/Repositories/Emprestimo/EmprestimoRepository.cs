using Livraria.Models;
using Livraria.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Livraria.Repository
{
    public class EmprestimoRepository : IEmprestimoRepository
    {
        private readonly LivrariaContext _context;

        public EmprestimoRepository(LivrariaContext context)
        {
            _context = context;
        }

        public void Add(Emprestimo emprestimo)
        {
            emprestimo.DataDevolucao = emprestimo.Data.AddDays(30);
            _context.Emprestimos.Add(emprestimo);
            _context.SaveChanges();
        }

        public Emprestimo Find(long id)
        {
            return _context.Emprestimos.Include("Usuario").Include("Livro").AsNoTracking().FirstOrDefault(a => a.Id == id);
        }
        
        public IEnumerable<EmprestimoVM> GetAll()
        {
            return _context.Emprestimos.Select(a => new EmprestimoVM() {
                Id = a.Id,
                LivroId = a.LivroId,
                Livro = a.Livro.Titulo,
                UsuarioId = a.UsuarioId,
                Usuario = a.Usuario.Nome,
                Data = a.Data,
                DataDevolucao = a.DataDevolucao,
                DiasRestantes = a.DataDevolucao.Subtract(DateTime.Now).Days,
                Devolucao = a.Devolucao
            }).ToList();
        }

        public void Remove(long id)
        {
            var entity = _context.Emprestimos.First(u => u.Id == id);
            _context.Emprestimos.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Emprestimo emprestimo)
        {
            _context.Emprestimos.Update(emprestimo);
            _context.SaveChanges();
        }

        public bool Exists(long id)
        {
            return _context.Emprestimos.Any(e => e.Id == id);
        }

        public bool QuantidadeLivrosByUsuario(long usuarioId)
        {
            return _context.Emprestimos.AsNoTracking().Where(a => a.UsuarioId == usuarioId && a.Devolucao == false).Count() >= 2;
        }

        public bool UsuarioBloqueado(long usuarioId)
        {
            return _context.Bloqueios.AsNoTracking().Any(a => a.UsuarioId == usuarioId && a.DataVencimento.Date <= DateTime.Now.Date);
        }

        public void Devolucao(long id)
        {
            var entity = _context.Emprestimos.First(u => u.Id == id);
            if (entity.DataDevolucao.Date < DateTime.Now.Date)
            {
                Bloqueio bloqueio = new Bloqueio();
                bloqueio.UsuarioId = entity.UsuarioId;
                bloqueio.DataVencimento = DateTime.Now.Date.AddDays(30);
                _context.Bloqueios.Add(bloqueio);
                _context.SaveChanges();
            }
            entity.Devolucao = true;
            _context.SaveChanges();
        }
    }
}
