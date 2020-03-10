using Livraria.Models;
using Livraria.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace TestLivraria
{
    public class InstituicaoTest
    {
        private readonly InstituicaoRepository repository;
        private readonly LivrariaContext context;
        public static DbContextOptions<LivrariaContext> dbContextOptions { get; }
        public static string connectionString = "Data Source=localhost;initial catalog=Livraria;persist security info=True;user id=sa;password=/password;multipleactiveresultsets=True;";

        static InstituicaoTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<LivrariaContext>()
                .UseSqlServer(connectionString)
                .Options;
        }

        public InstituicaoTest()
        {
            context = new LivrariaContext(dbContextOptions);
            repository = new InstituicaoRepository(context);

        }

        [Fact]
        public void TestCreate()
        {
            Instituicao instituicao = NovaInstituicao();
            repository.Add(instituicao);
            Assert.NotEqual(0, instituicao.Id);

            repository.Remove(instituicao.Id);
        }

        [Fact]
        public void TestUpdate()
        {
            Instituicao instituicao = NovaInstituicao();
            repository.Add(instituicao);
            var nome = instituicao.Nome;
            Assert.NotEqual(0, instituicao.Id);

            instituicao.Nome = "";
            repository.Update(instituicao);
            Assert.NotEqual(nome, instituicao.Nome);

            repository.Remove(instituicao.Id);
        }

        [Fact]
        public void TestDelete()
        {
            Instituicao instituicao = NovaInstituicao();
            repository.Add(instituicao);
            Assert.NotEqual(0, instituicao.Id);

            repository.Remove(instituicao.Id);
            var consulta = repository.Find(instituicao.Id);
            Assert.Null(consulta);
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
    }
}
