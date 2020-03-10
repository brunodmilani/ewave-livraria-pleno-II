using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Livraria.Models;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly LivrariaContext _context;

        public UsuarioRepository(LivrariaContext context)
        {
            _context = context;
        }

        public void Add(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public Usuario Find(long id)
        {
            return _context.Usuarios.AsNoTracking().FirstOrDefault(a => a.Id == id);
        }
        
        public IEnumerable<Usuario> GetAll()
        {
            return _context.Usuarios.ToList();
        }

        public void Remove(long id)
        {
            var entity = _context.Usuarios.First(u => u.Id == id);
            _context.Usuarios.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
        }

        public bool Exists(long id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
