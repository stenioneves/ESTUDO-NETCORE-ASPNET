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
                if (id == 0)
                {
                    HttpContext.Session.SetString("NomeUsuario",string.Empty);
                    HttpContext.Session.SetString("IdUsuario", string.Empty);
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
                return RedirectToAction("Sucesso");
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