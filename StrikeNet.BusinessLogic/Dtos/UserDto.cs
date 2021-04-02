using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StrikeNet.BusinessLogic.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public int Score { get; set; }
    }
}
