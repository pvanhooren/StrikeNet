using StrikeNet.EntityFramework.DbContext;
using StrikeNet.EntityFramework.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StrikeNet.EntityFramework.Entities
{
    public class Game
    {
        public string Team1 { get; set; }

        public string Team2 { get; set; }

        [Key] public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Result { get; set; }

        public TypeResult typeResult { get; set; }
    }
}
