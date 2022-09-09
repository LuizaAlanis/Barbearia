using Barbearia.Dados;
using Barbearia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Barbearia.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login

        // instanciando classe acoes / acoes uma classe do reposotorio para tratar as informações do banco(crud)
        acoesLogin acoes = new acoesLogin();


        // Criando a estrutura da página login
        public ActionResult Login()
        {
            return View();
        }
        // crianod o método http para verificar se o usuario e senha estão no banco 

        [HttpPost]
        public ActionResult Login(modelUsuario user)
        {
            // metodo que testa o usuario no banco
            acoes.TestarUsuario(user);

            if (user.usuario != null && user.senha != null)
            {
                // criado um metodo de criptografia apra verificar um objeto(nome e senha) e depois criada a sessão para validação.
                FormsAuthentication.SetAuthCookie(user.usuario, false);
                Session["usuarioLogado"] = user.usuario.ToString();
                Session["senhaLogado"] = user.senha.ToString();
                ViewBag.user = user.usuario.ToString();

                if (user.tipo == "1")
                {
                    Session["tipoLogado1"] = user.tipo.ToString();
                }
                else if (user.tipo == "2")
                {
                    Session["tipoLogado2"] = user.tipo.ToString();
                }
                // se o usuario e senha estiver corretos a pagina é direcionada para a página principal
                return RedirectToAction("Index", "Home");
            }
            else
            {    // caso o usuario e senha forem invalidos voltar para a tela login novamente.
                 // uma outra opção: Response.Write("<script>alert('Acesso Negado. Utilize um login e senha válidos')</script>");
                 // ainda outra opção: TempData["Message"] = "Acesso Negado. Utilize um login e senhas válidos";

                ViewBag.acessoNegado = "Acesso Negado. Utilize um login e senhas válidos";
                return View();
            }
        }
        //realizando o logout da página e pegando o usuario logado
        public ActionResult Logout()
        {
            Session["usuarioLogado"] = null;
            Session["senhaLogado"] = null;
            Session["tipoLogado1"] = null;
            Session["tipoLogado2"] = null;
            return RedirectToAction("Login", "Login");
        }

        public ActionResult acessoInvalido()
        {
            return View();
        }
    }
}