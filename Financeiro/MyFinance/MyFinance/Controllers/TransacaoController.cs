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

        public IActionResult Extrato()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
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