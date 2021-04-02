using System;
using System.Collections.Generic;
using System.Text;

namespace StrikeNet.BusinessLogic.Dtos
{
    public class UsersDto
    {
        public UsersDto()
        {
            Results = new List<UserDto>();
        }

        public List<UserDto> Results { get; set; }
    }
}
