using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Barbearia.Models
{
    public class modelCliente
    {
        [Display(Name = "Código")]
        public string codCli { get; set; }

        [Display(Name = "Nome")]
        public string nomeCli { get; set; }

        [Display(Name = "Telefone")]
        public string telefoneCli { get; set; }

        [Display(Name = "Celular")]
        public string celularCli { get; set; }

        [Display(Name = "Email")]
        public string emailCli { get; set; }

    }
}