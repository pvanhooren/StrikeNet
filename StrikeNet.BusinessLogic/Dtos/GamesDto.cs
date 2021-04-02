using System;
using System.Collections.Generic;
using System.Text;

namespace StrikeNet.BusinessLogic.Dtos
{
    public class GamesDto
    {
        public GamesDto()
        {
            AllGames = new List<GameDto>();
        }

        public List<GameDto> AllGames { get; set; }
    }
}
