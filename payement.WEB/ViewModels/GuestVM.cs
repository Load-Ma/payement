using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Payement.WEB.ViewModels
{
    public class GuestVM
    {
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        public int Party_ID { get; set; }

        [Required(ErrorMessage = "Ce champ est obligatoire")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Ce champ est obligatoire")]
        public int Spent { get; set; }

    }
}
