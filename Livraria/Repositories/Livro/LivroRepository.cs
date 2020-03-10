using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Livraria.Models;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Repository
{
    public class LivroRepository : ILivroRepository
    {
        private readonly LivrariaContext _context;

        public LivroRepository(LivrariaContext context)
        {
            _context = context;
        }

        public void Add(Livro livro)
        {
            _context.Livros.Add(livro);
            _context.SaveChanges();
        }

        public Livro Find(long id)
        {
            return _context.Livros.Include("Autor").Include("Genero").AsNoTracking().FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Livro> GetAll()
        {
            return _context.Livros.Include("Autor").Include("Genero").ToList();
        }

        public IEnumerable<Livro> GetNotEmprestado() //Carrega apenas livros nao emprestados
        {
            var livrosEmprestados = _context.Emprestimos.Where(a => a.Devolucao == false).Select(a => a.LivroId).ToList();

            return _context.Livros.Where(a => !livrosEmprestados.Contains(a.Id)).Include("Autor").Include("Genero").ToList();
        }

        public void Remove(long id)
        {
            var entity = _context.Livros.First(u => u.Id == id);
            _context.Livros.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Livro autor)
        {
            _context.Livros.Update(autor);
            _context.SaveChanges();
        }

        public bool Exists(long id)
        {
            return _context.Livros.Any(e => e.Id == id);
        }
    }
}
