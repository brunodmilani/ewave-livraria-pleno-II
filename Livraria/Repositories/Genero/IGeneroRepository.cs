using Livraria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Repository
{
    public interface IGeneroRepository
    {
        void Add(Genero genero);
        IEnumerable<Genero> GetAll();
        Genero Find(long id);
        void Remove(long id);
        void Update(Genero genero);
        bool Exists(long id);
    }
}
