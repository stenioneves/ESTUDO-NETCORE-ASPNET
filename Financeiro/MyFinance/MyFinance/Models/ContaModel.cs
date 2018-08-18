using MyFinance.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MyFinance.Models
{
    public class ContaModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Saldo { get; set; }
        public int Id_usuario { get; set; }
        IHttpContextAccessor HttpContextAccessor;
        public ContaModel()
        {

        }
        // Recebe o contexto para acesso às variáveis de sessão
        public ContaModel(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }
        public List<ContaModel> ListarConta()
        {
            List<ContaModel> lista = new List<ContaModel>();
            ContaModel item;

            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuario");
            string sql = $"SELECT idconta,nomeConta,saldo,Usuario_idUsuario FROM conta WHERE Usuario_idUsuario={id_usuario_logado} ";
            DAL dal = new DAL();
            DataTable dt = dal.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new ContaModel();
                item.Id = int.Parse(dt.Rows[i]["idconta"].ToString());
                item.Nome = dt.Rows[i]["nomeConta"].ToString();
                item.Saldo= double.Parse(dt.Rows[i]["saldo"].ToString());
                item.Id_usuario = int.Parse(dt.Rows[i]["Usuario_idUsuario"].ToString());
                lista.Add(item);

            }

            return lista;
         

        }

    }
}
