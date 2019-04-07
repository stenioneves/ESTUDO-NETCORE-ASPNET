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
        [HttpPost]
        public IActionResult NovaConta(ContaModel formulario)
        {

            if (ModelState.IsValid)

            {
                formulario.HttpContextAccessor = HttpContextAccessor;
                formulario.Insert();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult NovaConta()
        {
            return View();
        }

        public IActionResult ExcluirConta(int id)
        {
            ContaModel conta = new ContaModel(HttpContextAccessor);
            conta.Excluir(id);
            return RedirectToAction("Index");
        }
         /*Atualiza o saldo, mas verificar se é uma nova inserção ou alteração */
        public void AtualizarSaldo(int idconta,double valor, bool alter){
            ContaModel conta =new ContaModel(HttpContextAccessor);
            conta.Saldo= valor;// Nesse caso o saldo é o valor da transação para abater no saldo ou somar.
            conta.Id= idconta;

            conta.AtualizarSaldo(alter);
            Console.WriteLine($"alter-->>>{alter}");
        }
    }
}