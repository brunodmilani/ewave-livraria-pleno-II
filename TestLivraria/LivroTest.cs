using Livraria.Models;
using Livraria.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace TestLivraria
{
    public class LivroTest
    {
        private readonly LivrariaContext context;
        private readonly LivroRepository repository;
        private readonly GeneroRepository repositoryGenero;
        private readonly AutorRepository repositoryAutor;
        public static DbContextOptions<LivrariaContext> dbContextOptions { get; }
        public static string connectionString = "Data Source=localhost;initial catalog=Livraria;persist security info=True;user id=sa;password=/password;multipleactiveresultsets=True;";

        static LivroTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<LivrariaContext>()
                .UseSqlServer(connectionString)
                .Options;
        }

        public LivroTest()
        {
            context = new LivrariaContext(dbContextOptions);
            repository = new LivroRepository(context);
            repositoryGenero = new GeneroRepository(context);
            repositoryAutor = new AutorRepository(context);
        }

        [Fact]
        public void TestCreate()
        {
            Genero genero = NovoGenero();
            repositoryGenero.Add(genero);
            Assert.NotEqual(0, genero.Id);

            Autor autor = NovoAutor();
            repositoryAutor.Add(autor);
            Assert.NotEqual(0, autor.Id);
            
            Livro livro = NovoLivro(genero.Id, autor.Id);
            repository.Add(livro);
            Assert.NotEqual(0, livro.Id);
            repository.Remove(livro.Id);
            repositoryGenero.Remove(genero.Id);
            repositoryAutor.Remove(autor.Id);
        }

        [Fact]
        public void TestUpdate()
        {
            Genero genero = NovoGenero();
            repositoryGenero.Add(genero);
            Assert.NotEqual(0, genero.Id);

            Autor autor = NovoAutor();
            repositoryAutor.Add(autor);
            Assert.NotEqual(0, autor.Id);

            Livro livro = NovoLivro(genero.Id, autor.Id);
            repository.Add(livro);
            var titulo = livro.Titulo;
            Assert.NotEqual(0, livro.Id);

            livro.Titulo = "Descrição Livro - teste - alterado";
            repository.Update(livro);

            Assert.NotEqual(titulo, livro.Titulo);
            repository.Remove(livro.Id);
            repositoryGenero.Remove(genero.Id);
            repositoryAutor.Remove(autor.Id);
        }

        [Fact]
        public void TestDelete()
        {
            Genero genero = NovoGenero();
            repositoryGenero.Add(genero);
            Assert.NotEqual(0, genero.Id);

            Autor autor = NovoAutor();
            repositoryAutor.Add(autor);
            Assert.NotEqual(0, autor.Id);
            
            Livro livro = NovoLivro(genero.Id, autor.Id);
            repository.Add(livro);
            Assert.NotEqual(0, livro.Id);
            
            repository.Remove(livro.Id);
            var livroConsulta = repository.Find(livro.Id);
            Assert.Null(livroConsulta);
            repositoryGenero.Remove(genero.Id);
            repositoryAutor.Remove(autor.Id);
        }

        private static Livro NovoLivro(long genero, long autor)
        {
            return new Livro
            {
                Titulo = "Livro - teste",
                GeneroId = genero,
                AutorId = autor,
                Sinopse = "Sinopse Livro - teste",
                CapaPath = "Capa.jpg"
            };
        }

        private static Genero NovoGenero()
        {
            return new Genero
            {
                Nome = "Genero 1"
            };
        }

        private static Autor NovoAutor()
        {
            return new Autor
            {
                Nome = "Autor 1"
            };
        }
    }
}
