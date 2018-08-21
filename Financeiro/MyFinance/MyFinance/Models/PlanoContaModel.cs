using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MyFinance.Util;

namespace MyFinance.Models
{
    public class PlanoContaModel
    {
        public int Id { get; set; }

        [Required (ErrorMessage ="Campo descrição é obrigatório!") ]
        public string Descricao { get; set; }
        [Required (ErrorMessage ="Por favor preencha esse campo!")]
        public string Tipo { get; set; }
        public int Usuario_id { get; set; }
        public IHttpContextAccessor HttpContextAccessor { get; set; }

        public PlanoContaModel()
        {

        }

        public PlanoContaModel(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;

        }

        public List<PlanoContaModel> ListaPlanoContas()
        {

            List<PlanoContaModel> listas = new List<PlanoContaModel>();
            PlanoContaModel item;
            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuario");
            string sql = $"SELECT idPlano_contas,descricao,tipo,usuario_id FROM plano_contas WHERE usuario_id={id_usuario_logado}";
            DAL dal = new DAL();
            DataTable dt =dal.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new PlanoContaModel();
                item.Id = int.Parse(dt.Rows[i]["idPlano_Contas"].ToString());
                item.Descricao = dt.Rows[i]["descricao"].ToString();
                item.Tipo = dt.Rows[i]["tipo"].ToString();
                item.Usuario_id = int.Parse(dt.Rows[i]["usuario_id"].ToString());
                listas.Add(item);
            }
            return listas;
        }
        public void Insert()
        {
            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuario");
            string sql = "";
            if (Id == 0) { 
            sql = $"INSERT INTO plano_contas(descricao,tipo,usuario_id) VALUES('{Descricao}','{Tipo}','{id_usuario_logado}')";
            }
            else
            {
                sql = $"UPDATE plano_contas SET descricao='{Descricao}',tipo='{Tipo}' WHERE usuario_id='{id_usuario_logado}' AND idPlano_Contas='{Id}'";

            }
        DAL dal = new DAL();
            dal.ExecutarComandoSQL(sql);
        }


        public void Excluir(int id)
        {
            new DAL().ExecutarComandoSQL($"DELETE FROM plano_contas WHERE idPlano_Contas ={id}");
        }

        public PlanoContaModel CarregarDados(int? id)
        {
            PlanoContaModel registro = new PlanoContaModel();
            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuario");
            string sql = $"SELECT idPlano_contas,descricao,tipo,usuario_id FROM plano_contas WHERE usuario_id={id_usuario_logado} AND idPlano_contas ={id}";
            DAL dados = new DAL();
            DataTable dt = dados.RetDataTable(sql);
            registro.Id = int.Parse(dt.Rows[0]["idPlano_contas"].ToString());
            registro.Descricao = dt.Rows[0]["descricao"].ToString();
            registro.Tipo = dt.Rows[0]["tipo"].ToString();
            registro.Usuario_id = int.Parse(dt.Rows[0]["usuario_id"].ToString());
            return registro;

           
        }
    }
}
