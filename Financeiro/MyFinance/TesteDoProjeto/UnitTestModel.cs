using System;
using Xunit;
using MyFinance.Models;

namespace TesteDoProjeto
{
    public class UnitTestModel
    {
        [Fact]
        public void TestLoginUsuario()
        {
            UsuarioModel usr = new UsuarioModel();
            usr.Email = "teste@teste.com";
            usr.Senha = "1234";
            Assert.True(usr.ValidarLogin());

        }

        [Fact]
        public void TestCadastrarUsuario()
        {
            UsuarioModel usr = new UsuarioModel();
            usr.Nome = "teste";
            usr.Email = "teste@teste.com";
            usr.Senha = "1234";
            usr.DataNascimento = "2019/01/19";
            Assert.True(usr.ValidarLogin());

        }
    }
}
