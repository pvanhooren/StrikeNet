using StrikeNet.BusinessLogic.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StrikeNet.ViewModels
{
    public class VoorspellingViewModel
    {
        public GameDto Game { get; set; }

        public UserDto User { get; set; }

        public bool HasPredicted { get; set; }

        [Required]
        [StringLength(3, ErrorMessage = "Invoer is niet in correct format")]
        [RegularExpression("[0-9][-][0-9]", ErrorMessage = "Invoer is niet in correct format")]
        public string Prediction { get; set; }
    }
}
