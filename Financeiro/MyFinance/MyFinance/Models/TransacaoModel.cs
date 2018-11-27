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
    public class TransacaoModel
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="Favor informe a Data!")]
        public string Data { get; set; }
        public string DataFinal { get; set; } //Usado para gerar relatório
        public string Tipo { get; set; }
        [Required (ErrorMessage ="Informe o Valor!")]
        public double Valor { get; set; }
        public string Descricao { get; set; }
        public int IdConta { get; set; }
        public int IdPlanoConta { get; set; }
        public int IdUsuario { get; set; }

        public string Nomeconta { get; set; }
        public string DescricaoPlanoConta { get; set; }
        public IHttpContextAccessor HttpContextAccessor { get; set; }

        public TransacaoModel() { }

        public TransacaoModel(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public List<TransacaoModel> ListarTransacao()
        {
           
            List<TransacaoModel> lista = new List<TransacaoModel>();
            TransacaoModel item;
            //Usado no filtro
            string filtro = "";
            if ((Data != null) && (DataFinal != null))
            {
                filtro += $" and t.data >='{DateTime.Parse(Data).ToString("yyyy/MM/dd")}' and t.data <= '{DateTime.Parse(DataFinal).ToString("yyyy/MM/dd")}'";
            }
            if (Tipo != null)
            {
                if(Tipo != "A")
                {
                    filtro += $" and t.tipo='{Tipo}'";
                }
            }
            if (IdConta != 0)
            {
                filtro += $" and t.Conta_idconta='{IdConta}'";
            }
            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuario");
            string sql = "select t.idTransacao,t.data,t.tipo,t.valor,t.descricao as historico,t.Conta_idconta,c.nomeConta as conta,t.Plano_Contas_idPlano_Contas," +
                     " p.descricao as plano_conta from transacao AS t"+
                     $" INNER JOIN conta c ON t.Conta_idconta = c.idconta INNER JOIN plano_contas as p ON t.Plano_Contas_idPlano_Contas = p.idPlano_Contas Where t.usuario_id = {id_usuario_logado}" +
                    $"{filtro} ORDER BY t.data desc limit 10"; 
            DAL dal = new DAL();
            DataTable dt = dal.RetDataTable(sql);
            for (int i = 0; i <dt.Rows.Count ; i++)
            {
                item = new TransacaoModel();
                item.Id = int.Parse(dt.Rows[i]["idTransacao"].ToString());
                item.Data = DateTime.Parse(dt.Rows[i]["data"].ToString()).ToString("dd/MM/yyyy");
                item.Valor = double.Parse(dt.Rows[i]["valor"].ToString());
                item.Tipo = dt.Rows[i]["Tipo"].ToString();
                item.Descricao = dt.Rows[i]["historico"].ToString();
                item.IdConta= int.Parse(dt.Rows[i]["Conta_idconta"].ToString());
                item.Nomeconta = dt.Rows[i]["conta"].ToString();
                item.IdPlanoConta = int.Parse(dt.Rows[i]["Plano_Contas_idPlano_Contas"].ToString());
                item.DescricaoPlanoConta = dt.Rows[i]["plano_conta"].ToString();
                //item.IdUsuario = int.Parse(dt.Rows[i]["usuario_id"].ToString());
                lista.Add(item);
            }

            return lista;
        }


        public void insert()
        {
            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuario");
            string sql = "";
            if (Id == 0)
            {
                sql = $"INSERT INTO transacao (data,tipo,valor,descricao,Conta_idconta, Plano_Contas_idPlano_Contas,usuario_id) VALUES('{DateTime.Parse(Data).ToString("yyyy/MM/dd")}','{Tipo}','{Valor}','{Descricao}','{IdConta}','{IdPlanoConta}','{id_usuario_logado}')";
            }
            else
            {
                sql = $"UPDATE transacao SET data='{DateTime.Parse(Data).ToString("yyyy/MM/dd")}',"+
                    $" descricao='{Descricao}',tipo='{Tipo}', valor ='{Valor}'," +
                    $"Conta_idconta='{IdConta}', Plano_Contas_idPlano_Contas='{IdPlanoConta}' " +
                    $"WHERE usuario_id='{id_usuario_logado}' AND idTransacao='{Id}'";

            }
            DAL dal = new DAL();
            dal.ExecutarComandoSQL(sql);
        }
         public TransacaoModel CarregarDados(int? id)
        {
            
            TransacaoModel item = new TransacaoModel();
            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuario");
            string sql = "select t.idTransacao,t.data,t.tipo,t.valor,t.descricao as historico,t.Conta_idconta,c.nomeConta as conta,t.Plano_Contas_idPlano_Contas," +
                     " p.descricao as plano_conta from transacao AS t" +
                     $" INNER JOIN conta c ON t.Conta_idconta = c.idconta INNER JOIN plano_contas as p ON t.Plano_Contas_idPlano_Contas = p.idPlano_Contas Where t.usuario_id = {id_usuario_logado}" +
                    $" AND t.idTransacao={id}";
            DAL dal = new DAL();
            DataTable dt = dal.RetDataTable(sql);
            
            
                
                item.Id = int.Parse(dt.Rows[0]["idTransacao"].ToString());
                item.Data = DateTime.Parse(dt.Rows[0]["data"].ToString()).ToString("dd/MM/yyyy");
                item.Valor = double.Parse(dt.Rows[00]["valor"].ToString());
                item.Tipo = dt.Rows[0]["Tipo"].ToString();
                item.Descricao = dt.Rows[0]["historico"].ToString();
                item.IdConta = int.Parse(dt.Rows[0]["Conta_idconta"].ToString());
                item.Nomeconta = dt.Rows[0]["conta"].ToString();
                item.IdPlanoConta = int.Parse(dt.Rows[0]["Plano_Contas_idPlano_Contas"].ToString());
                item.DescricaoPlanoConta = dt.Rows[0]["plano_conta"].ToString();
            //item.IdUsuario = int.Parse(dt.Rows[i]["usuario_id"].ToString());



            return item;


        }

        public void Excluir(int id)
        {
            new DAL().ExecutarComandoSQL($"DELETE FROM transacao WHERE idTransacao ={id} ");

        }

    }







}
