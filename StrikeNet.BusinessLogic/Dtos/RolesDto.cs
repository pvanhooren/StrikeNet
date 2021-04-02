using System;
using System.Collections.Generic;
using System.Text;

namespace StrikeNet.BusinessLogic.Dtos
{
    public class RolesDto
    {
        public RolesDto()
        {
            Results = new List<RoleDto>();
        }

        public List<RoleDto> Results { get; set; }
    }
}
