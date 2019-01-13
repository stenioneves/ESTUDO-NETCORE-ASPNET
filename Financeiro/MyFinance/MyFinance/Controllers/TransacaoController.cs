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
            ViewBag.ListarContas = new ContaModel(HttpContextAccessor).ListarConta();
            return View();
        }

        [HttpGet]
        [HttpPost]
        public IActionResult Dashboard(Dashboard filtro)
        {
            filtro.HttpContextAccessor = HttpContextAccessor;
            ViewBag.ListarContas = new ContaModel(HttpContextAccessor).ListarConta();

            List<Dashboard> lista = new Dashboard().RetornarDadosGraficoPizza();

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


    }
}