using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Barbearia.Models
{
    public class modelBarbeiro
    {
        [Display(Name = "Código")]
        public string codBarbeiro { get; set; }

        [Display(Name = "Nome")]
        public string nomeBarbeiro { get; set; }
    }
}