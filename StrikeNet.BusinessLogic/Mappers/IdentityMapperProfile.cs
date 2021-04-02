using AutoMapper;
using StrikeNet.BusinessLogic.Dtos;
using StrikeNet.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrikeNet.BusinessLogic.Mappers
{
    public class IdentityMapperProfile : Profile
    {
        public IdentityMapperProfile()
        {
            CreateMap<RoleIdentity, RoleDto>(MemberList.Destination);

            CreateMap<List<RoleIdentity>, RolesDto>(MemberList.Destination)
                .ForMember(x => x.Results,
                    opt => opt.MapFrom(src => src));

            CreateMap<List<RoleIdentity>, UserRolesDto>(MemberList.Destination)
                .ForMember(x => x.Roles,
                    opt => opt.MapFrom(src => src));

            CreateMap<UserIdentity, UserDto>(MemberList.Destination);

            CreateMap<List<UserIdentity>, UsersDto>(MemberList.Destination)
                .ForMember(x => x.Results,
                    opt => opt.MapFrom(src => src));

            // model to entity
            CreateMap<RoleDto, RoleIdentity>(MemberList.Source);
            CreateMap<UserDto, UserIdentity>(MemberList.Source);
        }
    }
}
