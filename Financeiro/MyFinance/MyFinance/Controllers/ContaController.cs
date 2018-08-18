using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MyFinance.Models;

namespace MyFinance.Controllers
{
    public class ContaController : Controller
    {
        IHttpContextAccessor HttpContextAccessor;

        public ContaController(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()

        {
            ContaModel conta = new ContaModel(HttpContextAccessor);
            ViewBag.ListasContas = conta.ListarConta();

            return View();
        }
    }
}