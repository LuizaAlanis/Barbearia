using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Barbearia.Models
{
    public class modelUsuario
    {
        [Display(Name = "Usuário")]
        [Required(ErrorMessage = "Obrigatório digitar um nome !!")]
        public string usuario { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Obrigatório digitar a senha !!")]
        public string senha { get; set; }

        public string tipo { get; set; }
    }
}