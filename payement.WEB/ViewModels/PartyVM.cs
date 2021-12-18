using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Payement.WEB.ViewModels
{
    public class PartyVM
    {
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        public string Name { get; set; }
    }
}
