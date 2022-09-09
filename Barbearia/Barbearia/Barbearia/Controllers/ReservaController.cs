using Barbearia.Dados;
using Barbearia.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Barbearia.Controllers
{
    public class ReservaController : Controller
    {
        // GET: Reserva
        public ActionResult Index()
        {
            if ((Session["usuarioLogado"] == null) || (Session["senhaLogado"] == null))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                return View();
            }
        }

        acoesReserva ac = new acoesReserva();

        public void carregaClientes()
        {
            List<SelectListItem> ag = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server = localhost ; DataBase = bdBarbearia ; User = userBarbearia ; pwd = 1234567"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbCliente order by nomeCli;", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ag.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();
                con.Open();
            }


            ViewBag.cliente = new SelectList(ag, "Value", "Text");
        }









        public void carregaBarbeiros()
        {
            List<SelectListItem> dent = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server = localhost ; DataBase = bdBarbearia ; User = userBarbearia ; pwd = 1234567"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbBarbeiro order by nomeBarbeiro;", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    dent.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();
                con.Open();
            }


            ViewBag.barbeiro = new SelectList(dent, "Value", "Text");
        }

        public ActionResult excluirReserva(int id)
        {
            try
            {
                acoesReserva sdb = new acoesReserva();
                if (sdb.DeleteReserva(id))
                {
                    ViewBag.AlertMsg = "Reserva excluída com sucesso";
                }
                return RedirectToAction("listarReservasCons");
            }
            catch
            {
                return RedirectToAction("listarReservasCons");
            }
        }

        public ActionResult listarReservas()
        {
            ModelState.Clear();
            return View(ac.BuscarReserva());
        }

        public ActionResult editarReservas(string id)
        {
            carregaBarbeiros();
            carregaClientes();
            return View(ac.GetReservaCons().Find(smodel => smodel.codReserva == id));
        }

        [HttpPost]
        public ActionResult editarReservas(int id, modelReserva smodel)
        {
            try
            {
                carregaBarbeiros();
                carregaClientes();
                smodel.codCli = Request["cliente"];
                smodel.codBarbeiro = Request["barbeiro"];
                ac.TestarAgenda(smodel);

                if (smodel.confReserva == "1")
                {
                    ac.atualizaReserva(smodel);
                    ViewBag.msg = "Reagendamento feito";
                    return RedirectToAction("listarReservasCons");
                }
                else if (smodel.confReserva == "0")
                {
                    ViewBag.msg = "Horário indisponível";
                    return View();
                }
                return View();
            }
            catch
            {
                return View();
            }
        }


        public ActionResult listarReservasCons()
        {
            acoesReserva dbhandle = new acoesReserva();
            ModelState.Clear();
            return View(dbhandle.GetReservaCons());
        }

        public ActionResult cadReserva()
        {
            if ((Session["usuarioLogado"] == null) || (Session["senhaLogado"] == null))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                if (Session["tipoLogado1"] != null)
                {
                    carregaBarbeiros();
                    carregaClientes();
                    return View();
                }
                else
                {
                    return RedirectToAction("listarReservasCons");
                }
            }
        }

        [HttpPost]
        public ActionResult cadReserva(modelReserva at)
        {
            carregaBarbeiros();
            carregaClientes();
            at.codCli = Request["cliente"];
            at.codBarbeiro = Request["barbeiro"];
            ac.TestarAgenda(at);

            if (at.confReserva == "1")
            {
                ac.inserirReserva(at);
                ViewBag.msg = "Agendamento Realizado";
                return View();
            }
            else if (at.confReserva == "0")
            {
                ViewBag.msg = "Horário indisponível";
                return View();
            }

            return View();

        }
    }
}