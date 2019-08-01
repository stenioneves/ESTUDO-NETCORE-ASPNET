using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MyFinance.Models;

namespace MyFinance.Controllers
{
    public class TransacaoController : Controller
    {
        IHttpContextAccessor HttpContextAccessor;

        public TransacaoController(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            TransacaoModel transacao = new TransacaoModel(HttpContextAccessor);
            ViewBag.ListarTransacao = transacao.ListarTransacao();

            return View();
        }

        [HttpGet]
        [HttpPost]
        public IActionResult Extrato(TransacaoModel formulario)
        {
            formulario.HttpContextAccessor = HttpContextAccessor;
            ViewBag.ListarTransacao = formulario.ListarTransacao();
            ContaModel conta = new ContaModel(HttpContextAccessor);//Alteração necessária para não repetir código.
            ViewBag.ListarContas= conta.ListarConta();
              if(formulario.IdConta !=0){
                 conta.Id=formulario.IdConta;
            ViewBag.ContaSaldo = conta.ConsultarSaldo();
            
            }else{
                TempData["alerta"]="Você não aplicou o filtro,logo,os dados são de todas suas contas e transação!";
            }
            return View();
        }


        [HttpGet]
        [HttpPost]
        public IActionResult Dashboard(Dashboard filtro)
        {
            filtro.HttpContextAccessor = HttpContextAccessor;
            ViewBag.ListarContas = new ContaModel(HttpContextAccessor).ListarConta();

            List<Dashboard> lista = filtro.RetornarDadosGraficoPizza();

            string valores = "";
            string labels = "";
            string cores = "";
            var random = new Random();
            for (int i = 0; i < lista.Count; i++)
            {
                valores += lista[i].Total.ToString() + ",";
                labels += "'"+lista[i].PlanoContas.ToString() + "',";
                cores += "'" + String.Format("#{0:X6}", random.Next(0x1000000))+ "',";
            }



            ViewBag.Valores =valores ;
            ViewBag.labels = labels;
            ViewBag.cores = cores;
            return View();
        }
        [HttpPost]
        public IActionResult NovaTransacao( TransacaoModel formulario)
        {
            if (ModelState.IsValid)
            {
                formulario.HttpContextAccessor = HttpContextAccessor;
                formulario.insert();
                bool alter =false;
                if(formulario.Id !=0)//se o formulario conter id,logo é uma correção, logo o saldo não pode ser calculado de forma simples.
                     { alter=true;
                      TransacaoModel tm = formulario.CarregarDados(formulario.Id);
                        formulario.Valor =tm.Valor-formulario.Valor;


                     }
                // Verificação se a transação é uma despesa ou Receita
                if(formulario.Tipo.ToString().Equals("D")){
                
                    new ContaController(HttpContextAccessor).AtualizarSaldo(formulario.IdConta,formulario.Valor*(-1), alter);//Regra basica matemática;)
                }else{
                    new ContaController(HttpContextAccessor).AtualizarSaldo(formulario.IdConta,formulario.Valor,alter);
                }
                TempData["info"]=" Saldo de conta Atualizado com essa Transação!";
                 
                return  RedirectToAction("index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult NovaTransacao(int? id)
        {
            if(id != null)
            {
                TransacaoModel transacao = new TransacaoModel(HttpContextAccessor);
                ViewBag.Registro = transacao.CarregarDados(id);
            }
            ViewBag.ListarContas = new ContaModel(HttpContextAccessor).ListarConta();
            ViewBag.ListarPlanoConta = new PlanoContaModel(HttpContextAccessor).ListaPlanoContas();
            return View();
        }

        public IActionResult ExcluirTransacao(int id)
        {
            new TransacaoModel().Excluir(id);
            return RedirectToAction("Index");
        }


        public IActionResult Lancamento(int id){
        
         ViewBag.Transacao= new TransacaoModel().CarregarDados(id);


         }

    }
}