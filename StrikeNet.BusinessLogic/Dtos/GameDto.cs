using StrikeNet.EntityFramework.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StrikeNet.BusinessLogic.Dtos
{
    public class GameDto
    {
        public string Team1 { get; set; }

        public string Team2 { get; set; }

        public int Id { get; set; }

        public DateTime? Date { get; set; }

        public string Result { get; set; }

        public TypeResult typeResult { get; set; }
    }
}
