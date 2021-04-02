using AutoMapper;
using StrikeNet.BusinessLogic.Dtos;
using StrikeNet.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrikeNet.BusinessLogic.Mappers
{
    public static class IdentityMappers
    {
        static IdentityMappers()
        {
            Mapper = new MapperConfiguration(cfg => { cfg.AddProfile<IdentityMapperProfile>(); }).CreateMapper();
        }

        internal static IMapper Mapper { get; }

        public static RoleIdentity ToEntity(this RoleDto role)
        {
            return role == null ? null : Mapper.Map<RoleIdentity>(role);
        }

        public static RoleDto ToModel(this RoleIdentity role)
        {
            return role == null ? null : Mapper.Map<RoleDto>(role);
        }

        public static RolesDto ToModel(this List<RoleIdentity> roles)
        {
            return roles == null ? null : Mapper.Map<RolesDto>(roles);
        }

        public static UserRolesDto ToUserRoles(this List<RoleIdentity> roles)
        {
            return roles == null ? null : Mapper.Map<UserRolesDto>(roles);
        }


        public static UserIdentity ToEntity(this UserDto user)
        {
            return user == null ? null : Mapper.Map<UserIdentity>(user);
        }

        public static UserDto ToModel(this UserIdentity user)
        {
            return user == null ? null : Mapper.Map<UserDto>(user);
        }

        public static UsersDto ToModel(this List<UserIdentity> users)
        {
            return users == null ? null : Mapper.Map<UsersDto>(users);
        }
    }
}
