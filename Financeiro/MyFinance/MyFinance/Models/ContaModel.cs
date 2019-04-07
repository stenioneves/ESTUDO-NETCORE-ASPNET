﻿using MyFinance.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MyFinance.Models
{
    public class ContaModel
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="Favor informe o nome da Conta!")]
        public string Nome { get; set; }
        [Required(ErrorMessage ="Informe o Saldo!")]
        public double Saldo { get; set; }
        public int Id_usuario { get; set; }
        public IHttpContextAccessor HttpContextAccessor { get; set; }
        public ContaModel()
        {

        }
        //Pegar o usuario logado<redução de repetição de código>
         public string IdUsuarioLogado()
        {
            return HttpContextAccessor.HttpContext.Session.GetString("IdUsuario");

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

            
            string sql = $"SELECT idconta,nomeConta,saldo,Usuario_idUsuario FROM conta WHERE Usuario_idUsuario={IdUsuarioLogado()} ";
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

        internal void Excluir(int id)
        {
            new DAL().ExecutarComandoSQL($"DELETE FROM conta WHERE idconta={id}");
        }

        public void Insert()
        {
            string sql = $"INSERT INTO conta(nomeConta,saldo,Usuario_idUsuario) VALUES('{Nome}',{Saldo},{IdUsuarioLogado()})";
            DAL dal = new DAL();
            dal.ExecutarComandoSQL(sql);
        }
         /*
            
           Toda vez que houve uma inclusão ou atualização da modalidade Transação o saldo da conta 
           dever atualizado. 
             Author: Stenio  Dom 24/03/2019 13:56
          */
        public void AtualizarSaldo(bool alter ){
         DAL dAL= new DAL();

         if(alter==false)
          Saldo+=ConsultarSaldo();
          else{
              Saldo=ConsultarSaldo()-Saldo;
          }


         string sql =$"UPDATE conta SET saldo ='{Saldo}' where Idconta ='{Id}'";
         
         dAL.ExecutarComandoSQL(sql);

        }


        public double ConsultarSaldo(){
          DAL dAL= new DAL();
         string temp= dAL.Consulta($"select saldo from conta where IdConta='{Id}'");
         Console.WriteLine("Aqui>>>"+temp);
         double saldo= double.Parse(temp);
           
           return saldo;


        }

    }
}
