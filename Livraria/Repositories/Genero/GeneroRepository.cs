using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Livraria.Models;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Repository
{
    public class GeneroRepository : IGeneroRepository
    {
        private readonly LivrariaContext _context;

        public GeneroRepository(LivrariaContext context)
        {
            _context = context;
        }

        public void Add(Genero genero)
        {
            _context.Generos.Add(genero);
            _context.SaveChanges();
        }

        public Genero Find(long id)
        {
            return _context.Generos.AsNoTracking().FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Genero> GetAll()
        {
            return _context.Generos.ToList();
        }

        public void Remove(long id)
        {
            var entity = _context.Generos.First(u => u.Id == id);
            _context.Generos.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Genero genero)
        {
            _context.Generos.Update(genero);
            _context.SaveChanges();
        }

        public bool Exists(long id)
        {
            return _context.Autores.Any(e => e.Id == id);
        }
    }
}
