using Livraria.Models;
using Livraria.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace TestLivraria
{
    public class GeneroTest
    {
        private readonly GeneroRepository repository;
        private readonly LivrariaContext context;
        public static DbContextOptions<LivrariaContext> dbContextOptions { get; }
        public static string connectionString = "Data Source=localhost;initial catalog=Livraria;persist security info=True;user id=sa;password=/password;multipleactiveresultsets=True;";

        static GeneroTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<LivrariaContext>()
                .UseSqlServer(connectionString)
                .Options;
        }

        public GeneroTest()
        {
            context = new LivrariaContext(dbContextOptions);
            repository = new GeneroRepository(context);
        }

        [Fact]
        public void TestCreate()
        {
            Genero genero = NovoGenero();
            repository.Add(genero);
            Assert.NotEqual(0, genero.Id);

            repository.Remove(genero.Id);
        }

        [Fact]
        public void TestUpdate()
        {
            Genero genero = NovoGenero();
            repository.Add(genero);
            var nome = genero.Nome;
            Assert.NotEqual(0, genero.Id);

            genero.Nome = "";
            repository.Update(genero);
            Assert.NotEqual(nome, genero.Nome);

            repository.Remove(genero.Id);
        }

        [Fact]
        public void TestDelete()
        {
            Genero genero = NovoGenero();
            repository.Add(genero);
            Assert.NotEqual(0, genero.Id);

            repository.Remove(genero.Id);
            var generoConsulta = repository.Find(genero.Id);
            Assert.Null(generoConsulta);
        }

        private static Genero NovoGenero()
        {
            return new Genero
            {
                Nome = "Genero 1"
            };
        }
    }
}
