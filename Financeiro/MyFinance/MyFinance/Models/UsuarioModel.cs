using MyFinance.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }

        [Required (ErrorMessage ="Informe seu nome!")]
        public string Nome { get; set; }

        [Required(ErrorMessage ="Informe seu Email!")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage ="O e-mail informado é inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Informe uma Senha!")]
        public string Senha { get; set; }

        [Required(ErrorMessage ="Informe sua Data de Nascimento")]
        public string DataNascimento { get; set; }

        public bool ValidarLogin()
        {
            string sql = $"SELECT idUsuario,nome,dataNascimento FROM usuario Where email='{Email}' AND senha='{Senha}'  ";
            DAL dal = new DAL();
            DataTable dataTable = dal.RetDataTable(sql);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count == 1)
                {
                    Id = int.Parse(dataTable.Rows[0]["idUsuario"].ToString());
                    Nome = dataTable.Rows[0]["Nome"].ToString();
                    DataNascimento = dataTable.Rows[0]["dataNascimento"].ToString();
                    return true;
                }
            }
            return false;
        }

        public void Cadastrar()

        {
            string dataNascimento = DateTime.Parse(DataNascimento).ToString("yyyy/MM/dd");
            string sql = $"INSERT INTO usuario(nome,email,senha,dataNascimento) values ('{Nome}','{Email}','{Senha}','{dataNascimento}')";
            DAL dal = new DAL();
            dal.ExecutarComandoSQL(sql);

        }

    }
}
