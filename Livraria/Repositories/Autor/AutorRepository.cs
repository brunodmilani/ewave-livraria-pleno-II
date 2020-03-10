using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Livraria.Models;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Repository
{
    public class AutorRepository : IAutorRepository
    {
        private readonly LivrariaContext _context;

        public AutorRepository(LivrariaContext context)
        {
            _context = context;
        }

        public void Add(Autor autor)
        {
            _context.Autores.Add(autor);
            _context.SaveChanges();
        }

        public Autor Find(long id)
        {
            return _context.Autores.AsNoTracking().FirstOrDefault(a => a.Id == id);
        }
        
        public IEnumerable<Autor> GetAll()
        {
            return _context.Autores.ToList();
        }

        public void Remove(long id)
        {
            var entity = _context.Autores.First(u => u.Id == id);
            _context.Autores.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Autor autor)
        {
            _context.Autores.Update(autor);
            _context.SaveChanges();
        }

        public bool Exists(long id)
        {
            return _context.Autores.Any(e => e.Id == id);
        }
    }
}
