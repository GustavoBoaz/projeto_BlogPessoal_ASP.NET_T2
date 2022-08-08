using BlogAPI.Src.Contextos;
using BlogAPI.Src.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace BlogTeste.Contextos
{
    /// <summary>
    /// <para>Resumo: Classe para texte unitario de contexto de usuario</para>
    /// <para>Criado por: Generation</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 17/07/2022</para>
    /// </summary>
    [TestClass]
    public class UsuarioContextoTeste
    {

        #region Atributos

        private BlogPessoalContexto _contexto;

        #endregion


        #region Métodos

        [TestMethod]
        public void InserirNovoUsuarioRetornaUsuarioInserido()
        {
            // Ambiente
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
                .UseInMemoryDatabase(databaseName: "IMD_blog_gen_UCT1")
                .Options;

            _contexto = new BlogPessoalContexto(opt);

            // DADO - Dado que adiciono um usuario no sistema
            _contexto.Usuarios.Add(new Usuario
            {
                Nome = "Johnny Boaz",
                Email = "johnny@email.com",
                Foto = "URLDAFOTO",
                Senha = "134652"
            });
            _contexto.SaveChanges();

            // QUANDO - Quando eu pesquiso pelo e-mail do usuario adicionado
            var resultado = _contexto.Usuarios.FirstOrDefault(u => u.Email == "johnny@email.com");

            // ENTÃO - Então deve retornar resultado nao nulo
            Assert.IsNotNull(resultado);
            Assert.AreEqual("johnny@email.com", resultado.Email);

        }

        [TestMethod]
        public void LerListaDeUsuariosRetornaTresUsuarios()
        {
            // Ambiente
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
                .UseInMemoryDatabase(databaseName: "IMD_blog_gen_UCT2")
                .Options;

            _contexto = new BlogPessoalContexto(opt);

            // DADO - Dado que adiciono 3 usuarios novos no sistema
            _contexto.Usuarios.Add(new Usuario
            {
                Nome = "Pamela Boaz",
                Email = "pamela@email.com",
                Senha = "134652",
                Foto = "URLFOTOPAMELABOAZ",
            });
            _contexto.Usuarios.Add(new Usuario
            {
                Nome = "Mallu Boaz",
                Email = "mallu@email.com",
                Senha = "134652",
                Foto = "URLFOTOMALLUBOAZ",
            });
            _contexto.Usuarios.Add(new Usuario
            {
                Nome = "Catarina Boaz",
                Email = "catarina@email.com",
                Senha = "134652",
                Foto = "URLFOTOCATARINABOAZ",
            });
            _contexto.SaveChanges();

            // QUANDO - Quando eu pesquisar por todos os usuarios
            var resultado = _contexto.Usuarios.ToList();

            // ENTÃO - Então deve retornar uma lista com 3 usuarios
            Assert.AreEqual(3, resultado.Count);
        }

        [TestMethod]
        public void AtualizarUsuarioRetornaUsuarioAtualizado()
        {
            // Ambiente
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
                .UseInMemoryDatabase(databaseName: "IMD_blog_gen_UCT3")
                .Options;

            _contexto = new BlogPessoalContexto(opt);

            // DADO - Dado que adiciono um usuario no sistema
            _contexto.Usuarios.Add(new Usuario
            {
                Nome = "Zenildo Boaz",
                Email = "zenildo@email.com",
                Senha = "134652",
                Foto = "URLFOTOZENILDOBOAZ",
            });
            _contexto.SaveChanges();

            // E - E altero seu nome para Zenildo Rosa
            var auxiliar = _contexto.Usuarios.FirstOrDefault(u => u.Email == "zenildo@email.com");
            auxiliar.Nome = "Zenildo Rosa";
            _contexto.Usuarios.Update(auxiliar);
            _contexto.SaveChanges();

            // QUANDO - Quando pesquiso pelo nome Zenildo Rosa
            var resultado = _contexto.Usuarios.FirstOrDefault(u => u.Nome == "Zenildo Rosa");

            // ENTÃO - Então deve retornar resultado nao nulo
            Assert.IsNotNull(resultado);
        }

        [TestMethod]
        public void DeletaUsuarioRetornaUsuarioInesistente()
        {
            // Ambiente
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
                .UseInMemoryDatabase(databaseName: "IMD_blog_gen_UCT4")
                .Options;

            _contexto = new BlogPessoalContexto(opt);

            // DADO - Dado que adiciono um usuario no sistema
            _contexto.Usuarios.Add(new Usuario
            {
                Nome = "Neusa Boaz",
                Email = "neusa@email.com",
                Senha = "134652",
                Foto = "URLFOTONEUSABOAZ",
            });
            _contexto.SaveChanges();

            // QUANDO - Quando deleto o usuario inserido
            var auxiliar = _contexto.Usuarios.FirstOrDefault(u => u.Email == "neusa@email.com");
            _contexto.Usuarios.Remove(auxiliar);
            _contexto.SaveChanges();

            // E - E pesquiso pelo nome Neusa Boaz
            var resultado = _contexto.Usuarios.FirstOrDefault(u => u.Nome == "Neusa Boaz");

            // ENTÃO - Então deve retornar resultado nulo
            Assert.IsNull(resultado);
        }

        #endregion
    }

}