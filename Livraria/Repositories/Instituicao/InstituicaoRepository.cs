using Livraria.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Livraria.Repository
{
    public class InstituicaoRepository : IInstituicaoRepository
    {
        private readonly LivrariaContext _context;

        public InstituicaoRepository(LivrariaContext context)
        {
            _context = context;
        }

        public void Add(Instituicao instituicao)
        {
            _context.Instituicoes.Add(instituicao);
            _context.SaveChanges();
        }

        public Instituicao Find(long id)
        {
            return _context.Instituicoes.AsNoTracking().FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Instituicao> GetAll()
        {
            return _context.Instituicoes.ToList();
        }

        public void Remove(long id)
        {
            var entity = _context.Instituicoes.First(u => u.Id == id);
            _context.Instituicoes.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Instituicao instituicao)
        {
            _context.Instituicoes.Update(instituicao);
            _context.SaveChanges();
        }

        public bool Exists(long id)
        {
            return _context.Instituicoes.Any(e => e.Id == id);
        }
    }
}
