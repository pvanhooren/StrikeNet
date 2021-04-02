using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StrikeNet.ViewModels
{
    public class GameViewModel
    {
        [Required(ErrorMessage = "Team 1 is een verplicht invoerveld")]
        public string Team1 { get; set; }

        [Required(ErrorMessage = "Team 2 is een verplicht invoerveld")]
        public string Team2 { get; set; }

        public int Id { get; set; }

        [Required(ErrorMessage = "Voer een geldige datum in")]
        public DateTime? Date { get; set; }

        [StringLength(3, ErrorMessage = "Invoer is niet in correct format")]
        [RegularExpression("[0-9][-][0-9]", ErrorMessage = "Uitslag is niet in correct format")]
        public string Result { get; set; }
    }
}
