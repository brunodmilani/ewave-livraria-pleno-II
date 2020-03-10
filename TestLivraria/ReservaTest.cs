using Livraria.Models;
using Livraria.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace TestLivraria
{
    public class ReservaTest
    {
        private readonly ReservaRepository repository;
        private readonly UsuarioRepository repositoryUsuario;
        private readonly LivroRepository repositoryLivro;
        private readonly GeneroRepository repositoryGenero;
        private readonly AutorRepository repositoryAutor;
        private readonly LivrariaContext context;
        public static DbContextOptions<LivrariaContext> dbContextOptions { get; }
        public static string connectionString = "Data Source=localhost;initial catalog=Livraria;persist security info=True;user id=sa;password=/password;multipleactiveresultsets=True;";

        static ReservaTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<LivrariaContext>()
                .UseSqlServer(connectionString)
                .Options;
        }

        public ReservaTest()
        {
            context = new LivrariaContext(dbContextOptions);
            repository = new ReservaRepository(context);
            repositoryUsuario = new UsuarioRepository(context);
            repositoryLivro = new LivroRepository(context);
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
            repositoryLivro.Add(livro);
            Assert.NotEqual(0, livro.Id);

            Usuario usuario = NovoUsuario();
            repositoryUsuario.Add(usuario);
            Assert.NotEqual(0, usuario.Id);

            Reserva reserva = NovoReserva(usuario.Id, livro.Id);
            repository.Add(reserva);
            Assert.NotEqual(0, reserva.Id);

            repository.Remove(reserva.Id);
            repositoryUsuario.Remove(usuario.Id);
            repositoryLivro.Remove(livro.Id);
            repositoryAutor.Remove(autor.Id);
            repositoryGenero.Remove(genero.Id);
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
            repositoryLivro.Add(livro);
            Assert.NotEqual(0, livro.Id);

            Livro livro2 = NovoLivro(genero.Id, autor.Id);
            livro2.Titulo = "Livro 2";
            repositoryLivro.Add(livro2);
            Assert.NotEqual(0, livro2.Id);

            Usuario usuario = NovoUsuario();
            repositoryUsuario.Add(usuario);
            Assert.NotEqual(0, usuario.Id);

            Reserva reserva = NovoReserva(usuario.Id, livro.Id);
            repository.Add(reserva);
            var livroOld = reserva.LivroId;
            Assert.NotEqual(0, reserva.Id);

            reserva.LivroId = livro2.Id;
            repository.Update(reserva);
            Assert.NotEqual(livroOld, reserva.LivroId);

            repository.Remove(reserva.Id);
            repositoryUsuario.Remove(usuario.Id);
            repositoryLivro.Remove(livro.Id);
            repositoryLivro.Remove(livro2.Id);
            repositoryAutor.Remove(autor.Id);
            repositoryGenero.Remove(genero.Id);
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
            repositoryLivro.Add(livro);
            Assert.NotEqual(0, livro.Id);

            Usuario usuario = NovoUsuario();
            repositoryUsuario.Add(usuario);
            Assert.NotEqual(0, usuario.Id);

            Reserva reserva = NovoReserva(usuario.Id, livro.Id);
            repository.Add(reserva);
            Assert.NotEqual(0, reserva.Id);

            repository.Remove(reserva.Id);
            var consulta = repository.Find(reserva.Id);
            Assert.Null(consulta);

            repositoryUsuario.Remove(usuario.Id);
            repositoryLivro.Remove(livro.Id);
            repositoryAutor.Remove(autor.Id);
            repositoryGenero.Remove(genero.Id);
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

        private static Usuario NovoUsuario()
        {
            Usuario usuario = new Usuario();
            usuario.Nome = "Instituicao";
            usuario.Endereco = "Rua 200";
            usuario.CPF = "000.000.000-00";
            usuario.Telefone = "(65) 1234-1234";
            usuario.Email = "teste@teste.com.br";
            return usuario;
        }

        private static Reserva NovoReserva(long usuario, long livro)
        {
            Reserva reserva = new Reserva();
            reserva.LivroId = livro;
            reserva.UsuarioId = usuario;
            return reserva;
        }
    }
}
