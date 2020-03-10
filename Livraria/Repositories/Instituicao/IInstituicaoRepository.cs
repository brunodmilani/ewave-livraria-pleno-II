using Livraria.Models;
using System.Collections.Generic;

namespace Livraria.Repository
{
    public interface IInstituicaoRepository
    {
        void Add(Instituicao instituicaoEnsino);
        IEnumerable<Instituicao> GetAll();
        Instituicao Find(long id);
        void Remove(long id);
        void Update(Instituicao instituicaoEnsino);
        bool Exists(long id);
    }
}
