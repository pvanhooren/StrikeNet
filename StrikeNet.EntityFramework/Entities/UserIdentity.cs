using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrikeNet.EntityFramework.Entities
{
    public class UserIdentity : IdentityUser<Guid>
    {
        public int Score { get; set; }
    }
}
