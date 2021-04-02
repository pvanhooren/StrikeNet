using StrikeNet.BusinessLogic.Dtos.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrikeNet.BusinessLogic.Dtos
{
    public class UserRolesDto
    {
        public UserRolesDto()
        {
            Roles = new List<RoleDto>();
        }

        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public List<SelectedItemDto> RolesList { get; set; }

        public List<RoleDto> Roles { get; set; }
    }
}
