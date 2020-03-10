using Livraria.Models;
using Livraria.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace TestLivraria
{
    public class UsuarioTest
    {
        private readonly UsuarioRepository repository;
        private readonly InstituicaoRepository instituicaoRepository;
        private readonly LivrariaContext context;
        public static DbContextOptions<LivrariaContext> dbContextOptions { get; }
        public static string connectionString = "Data Source=localhost;initial catalog=Livraria;persist security info=True;user id=sa;password=/password;multipleactiveresultsets=True;";

        static UsuarioTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<LivrariaContext>()
                .UseSqlServer(connectionString)
                .Options;
        }

        public UsuarioTest()
        {
            context = new LivrariaContext(dbContextOptions);
            repository = new UsuarioRepository(context);
            instituicaoRepository = new InstituicaoRepository(context);

        }

        [Fact]
        public void TestCreate()
        {
            Instituicao instituicao = NovaInstituicao();
            instituicaoRepository.Add(instituicao);
            Assert.NotEqual(0, instituicao.Id);
            
            Usuario usuario = NovoUsuario(instituicao.Id);
            repository.Add(usuario);
            Assert.NotEqual(0, usuario.Id);
            repository.Remove(usuario.Id);
            instituicaoRepository.Remove(instituicao.Id);
        }

        [Fact]
        public void TestUpdate()
        {
            Instituicao instituicao = NovaInstituicao();
            instituicaoRepository.Add(instituicao);
            Assert.NotEqual(0, instituicao.Id);
            
            Usuario usuario = NovoUsuario(instituicao.Id);
            repository.Add(usuario);
            var nome = usuario.Nome;
            Assert.NotEqual(0, usuario.Id);

            usuario.Nome = "Teste";
            repository.Update(usuario);
            Assert.NotEqual(nome, usuario.Nome);
            repository.Remove(usuario.Id);
            instituicaoRepository.Remove(instituicao.Id);
        }

        [Fact]
        public void TestDelete()
        {
            Instituicao instituicao = NovaInstituicao();
            instituicaoRepository.Add(instituicao);
            Assert.NotEqual(0, instituicao.Id);

            Usuario usuario = NovoUsuario(instituicao.Id);
            repository.Add(usuario);
            Assert.NotEqual(0, usuario.Id);

            repository.Remove(usuario.Id);
            var consulta = repository.Find(usuario.Id);
            Assert.Null(consulta);

            instituicaoRepository.Remove(instituicao.Id);
        }

        private static Instituicao NovaInstituicao()
        {
            Instituicao instituicao = new Instituicao();
            instituicao.Nome = "Instituicao";
            instituicao.Endereco = "Rua 200";
            instituicao.CNPJ = "59.174.248/0001-11";
            instituicao.Telefone = "(65) 1234-1234";
            return instituicao;
        }

        private static Usuario NovoUsuario(long? instituicao)
        {
            Usuario usuario = new Usuario();
            usuario.Nome = "Instituicao";
            usuario.Endereco = "Rua 200";
            usuario.CPF = "000.000.000-00";
            usuario.Telefone = "(65) 1234-1234";
            usuario.InstituicaoId = instituicao;
            usuario.Email = "teste@teste.com.br";
            return usuario;
        }
    }
}
