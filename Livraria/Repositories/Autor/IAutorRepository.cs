using Livraria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Repository
{
    public interface IAutorRepository
    {
        void Add(Autor autor);
        IEnumerable<Autor> GetAll();
        Autor Find(long id);
        void Remove(long id);
        void Update(Autor autor);
        bool Exists(long id);
    }
}
