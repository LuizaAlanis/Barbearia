using Barbearia.Dados;
using Barbearia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Barbearia.Controllers
{
    public class ClienteController : Controller
    {
        acoesCliente ac = new acoesCliente();

        // GET: Cliente
        public ActionResult Index()
        {
            if ((Session["usuarioLogado"] == null) || (Session["senhaLogado"] == null))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ModelState.Clear();
                return View(ac.BuscarCliente());
            }
        }

        public ActionResult cadCliente()
        {
            if ((Session["usuarioLogado"] == null) || (Session["senhaLogado"] == null))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                if (Session["tipoLogado1"] != null)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
        }


        [HttpPost]
        public ActionResult cadCliente(modelCliente at)
        {
            try
            {
                ac.inserirCliente(at);
                ViewBag.msg = "Cliente cadastrado";
                return View();
            }
            catch
            {
                ViewBag.msg = "Não foi possivel cadastrar";
                return View();
            }
        }

        public ActionResult atualizarCliente(string id)
        {
            if ((Session["usuarioLogado"] == null) || (Session["senhaLogado"] == null))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                return View(ac.BuscarCliente().Find(smodel => smodel.codCli == id));
            }
        }


        [HttpPost]
        public ActionResult atualizarCliente(int id, modelCliente smodel)
        {
            try
            {
                ac.atualizaCliente(smodel);
                return RedirectToAction("index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult excluirCliente(int id)
        {
            try
            {
                if (ac.DeleteCliente(id))
                {
                    ViewBag.AlertMsg = "Cliente excluído com sucesso";
                }
                return RedirectToAction("index");
            }
            catch
            {
                return RedirectToAction("index");
            }
        }


    }
}