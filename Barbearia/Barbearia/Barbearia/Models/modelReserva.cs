using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Barbearia.Models
{
    public class modelReserva
    {
        [Display(Name = "Código da reserva")]
        public string codReserva { get; set; }

        [Display(Name = "Código do cliente")]
        public string codCli { get; set; }

        [Display(Name = "Código do barbeiro")]
        public string codBarbeiro { get; set; }

        [Display(Name = "Data")]
        public string dataReserva { get; set; }

        [Display(Name = "Data")]
        public string dataReservaF { get; set; }

        [Display(Name = "Hora")]
        public string horaReserva { get; set; }

        public string confReserva { get; set; }
        
        [Display(Name = "Cliente")]
        public string nomeCli { get; set; }

        [Display(Name = "Barbeiro")]
        public string nomeBarbeiro { get; set; } 
    }
}