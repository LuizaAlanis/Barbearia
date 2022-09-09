using Barbearia.Dados;
using Barbearia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Barbearia.Controllers
{
    public class BarbeiroController : Controller
    {
        acoesBarbeiro ac = new acoesBarbeiro();

        // GET: Barbeiro
        public ActionResult Index()
        {
            if ((Session["usuarioLogado"] == null) || (Session["senhaLogado"] == null))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ModelState.Clear();
                return View(ac.BuscarBarbeiro());
            }
        }

        public ActionResult cadBarbeiro()
        {
            if ((Session["usuarioLogado"] == null) || (Session["senhaLogado"] == null))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                if(Session["tipoLogado1"] != null)
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
        public ActionResult cadBarbeiro(modelBarbeiro at)
        {
            try
            {
                ac.inserirBarbeiro(at);
                ViewBag.msg = "Barbeiro cadastrado";
                return View();
            }
            catch
            {
                ViewBag.msg = "Não foi possivel cadastrar";
                return View();
            }
        }

        public ActionResult atualizarBarbeiro(string id)
        {
            if ((Session["usuarioLogado"] == null) || (Session["senhaLogado"] == null))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                if (Session["tipoLogado1"] != null)
                {
                    return View(ac.BuscarBarbeiro().Find(smodel => smodel.codBarbeiro == id));
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
        }


        [HttpPost]
        public ActionResult atualizarBarbeiro(int id, modelBarbeiro smodel)
        {
            try
            {
                ac.atualizaBarbeiro(smodel);
                return RedirectToAction("index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult excluirBarbeiro(int id)
        {
            if ((Session["usuarioLogado"] == null) || (Session["senhaLogado"] == null))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                if (Session["tipoLogado1"] != null)
                {
                    try
                    {
                        if (ac.DeleteBarbeiro(id))
                        {
                            ViewBag.AlertMsg = "Barbeiro excluído com sucesso";
                        }
                        return RedirectToAction("index");
                    }
                    catch
                    {
                        return RedirectToAction("index");
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
        }
    }
}