using Livraria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Repository
{
    public interface IUsuarioRepository
    {
        void Add(Usuario autor);
        IEnumerable<Usuario> GetAll();
        Usuario Find(long id);
        void Remove(long id);
        void Update(Usuario autor);
        bool Exists(long id);
    }
}
