using Livraria.Models;
using Livraria.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace TestLivraria
{
    public class AutorTest
    {
        private readonly AutorRepository repository;
        private readonly LivrariaContext context;
        public static DbContextOptions<LivrariaContext> dbContextOptions { get; }
        public static string connectionString = "Data Source=localhost;initial catalog=Livraria;persist security info=True;user id=sa;password=/password;multipleactiveresultsets=True;";

        static AutorTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<LivrariaContext>()
                .UseSqlServer(connectionString)
                .Options;
        }

        public AutorTest()
        {
            context = new LivrariaContext(dbContextOptions);
            repository = new AutorRepository(context);
        }

        [Fact]
        public void TestCreate()
        {
            Autor autor = NovoAutor();
            repository.Add(autor);
            Assert.NotEqual(0, autor.Id);

            repository.Remove(autor.Id);
        }

        [Fact]
        public void TestUpdate()
        {
            Autor autor = NovoAutor();
            repository.Add(autor);
            var nome = autor.Nome;
            Assert.NotEqual(0, autor.Id);

            autor.Nome = "alteração";
            repository.Update(autor);
            Assert.NotEqual(nome, autor.Nome);

            repository.Remove(autor.Id);
        }

        [Fact]
        public void TestDelete()
        {
            Autor autor = NovoAutor();
            repository.Add(autor);
            Assert.NotEqual(0, autor.Id);

            repository.Remove(autor.Id);
            var consulta = repository.Find(autor.Id);
            Assert.Null(consulta);
        }

        private static Autor NovoAutor()
        {
            Autor autor = new Autor();
            autor.Nome = "Autor - teste";
            return autor;
        }
    }
}
