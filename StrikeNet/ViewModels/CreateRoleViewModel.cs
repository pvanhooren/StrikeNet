using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StrikeNet.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required(ErrorMessage = "Rol Naam is een verplicht invoerveld")]
        public string RoleName { get; set; }
    }
}
