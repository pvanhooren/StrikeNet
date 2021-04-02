using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StrikeNet.ViewModels
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            Users = new List<string>();
        }
        public Guid? Id { get; set; }

        [Required(ErrorMessage= "Rol Naam is een verplicht invoerveld")]
        public string RoleName { get; set; }

        public List<string> Users { get; set; }
    }
}
