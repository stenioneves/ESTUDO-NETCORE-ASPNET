using MyFinance.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataNascimento { get; set; }

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
                    DataNascimento = DateTime.Parse(dataTable.Rows[0]["dataNascimento"].ToString());
                    return true;
                }
            }
            return false;
        }

    }
}
