using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MyFinance.Models;

namespace MyFinance.Controllers
{
    public class PlanoContaController : Controller
    {
        IHttpContextAccessor HttpContextAccessor;

        public PlanoContaController(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            PlanoContaModel planoConta = new PlanoContaModel(HttpContextAccessor);
            ViewBag.ListarPlanocontas = planoConta.ListaPlanoContas();

            return View();

        }
        [HttpGet]
        public IActionResult NovoPlanoConta(int? id)
        {
            if(id != null)
            {
                PlanoContaModel planoConta = new PlanoContaModel(HttpContextAccessor);
                ViewBag.Registro = planoConta.CarregarDados( id);
            }

            return View();
        }
        [HttpPost]
        public IActionResult NovoPlanoConta(PlanoContaModel planoConta)
        {

            if (ModelState.IsValid)
            {
                planoConta.HttpContextAccessor = HttpContextAccessor;
                planoConta.Insert();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult ExcluirPlanoConta(int id)
        {
            PlanoContaModel planoConta = new PlanoContaModel(HttpContextAccessor);
            planoConta.Excluir(id);
            return RedirectToAction("Index");
        }

    }
}