using Livraria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Repository
{
    public interface ILivroRepository
    {
        void Add(Livro livro);
        IEnumerable<Livro> GetAll();
        IEnumerable<Livro> GetNotEmprestado();
        Livro Find(long id);
        void Remove(long id);
        void Update(Livro livro);
        bool Exists(long id);
    }
}
