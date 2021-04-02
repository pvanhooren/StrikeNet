using StrikeNet.BusinessLogic.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StrikeNet.ViewModels
{
    public class HomePageViewModel
    {
        public List<GameDto> Games = new List<GameDto>();

        public List<UserDto> Users = new List<UserDto>();
    }
}
