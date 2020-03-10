using Microsoft.EntityFrameworkCore;
using Livraria.Models;

namespace Livraria.Models
{
    public class LivrariaContext : DbContext
    {
        public LivrariaContext(DbContextOptions<LivrariaContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Instituicao> Instituicoes { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Emprestimo> Emprestimos { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Bloqueio> Bloqueios { get; set; }
    }
}
