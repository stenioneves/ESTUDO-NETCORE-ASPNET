using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Models;

namespace MyFinance.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public IActionResult Login(int? id)
        {   
            if(id != null)
            {
                ViewBag.sair = 1;
                if (id == 0)
                {
                    HttpContext.Session.SetString("NomeUsuario",string.Empty);
                    HttpContext.Session.SetString("IdUsuario", string.Empty);
                    ViewBag.sair = 0;
                }
            }
             
            return View();
        }

        [HttpPost]
        public IActionResult ValidarLogin(UsuarioModel usuario)
        {
            if (usuario.ValidarLogin())
            {

                HttpContext.Session.SetString("NomeUsuario", usuario.Nome);
                HttpContext.Session.SetString("IdUsuario", usuario.Id.ToString());
                return RedirectToAction("Menu", "Home");
            }
            else
            {
                TempData["Erro"] = " Usuário ou senha inválidos!";
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public IActionResult Cadastrar(UsuarioModel usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.Cadastrar();
                TempData["Msg"] = " Cadastro realizado com sucesso!" +
                    "Agora acesse o sistema com os dados informados ;)";
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        public IActionResult Sucesso()
        {
            return View();
        }

    }
}