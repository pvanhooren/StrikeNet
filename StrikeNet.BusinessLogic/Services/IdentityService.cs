using Microsoft.AspNetCore.Identity;
using StrikeNet.BusinessLogic.Dtos;
using StrikeNet.BusinessLogic.Dtos.Shared;
using StrikeNet.BusinessLogic.Mappers;
using StrikeNet.BusinessLogic.Services.Interface;
using StrikeNet.EntityFramework.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StrikeNet.BusinessLogic.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IIdentityRepository _identityRepository;
        //Roles
        public IdentityService(IIdentityRepository identityRepository)
        {
            _identityRepository = identityRepository;
        }

        public virtual async Task<bool> ExistsUserAsync(Guid? userId)
        {
            var exists = await _identityRepository.ExistsUserAsync(userId);
            if (!exists)
            {
                return false;
            }

            return true;
        }

        public virtual async Task<bool> ExistsRoleAsync(Guid? roleId)
        {
            var exists = await _identityRepository.ExistsRoleAsync(roleId);
            if (!exists)
            {
                return false;
            }

            return true;
        }


        public async Task<Guid?> CreateRoleAsync(RoleDto role)
        {
            var canInsert = await CanInsertRoleAsync(role);
            if (!canInsert)
            {
                return null;
            }

            var roleIdentity = role.ToEntity();

            var result = await _identityRepository.CreateRoleAsync(roleIdentity);
            return result.roleId;
        }

        public async Task<IdentityResult> DeleteRoleAsync(RoleDto role)
        {
            var roleDto = await GetRoleAsync(role.Id);

            var roleIdentityEntity = roleDto.ToEntity();

            return await _identityRepository.DeleteRoleAsync(roleIdentityEntity);
        }

        public async Task<IdentityResult> UpdateRoleAsync(RoleDto role)
        {
            var canInsert = await CanInsertRoleAsync(role);

            var roleIdentityEntity = role.ToEntity();

            return await _identityRepository.UpdateRoleAsync(roleIdentityEntity);
        }

        public async Task<bool> CanInsertRoleAsync(RoleDto role)
        {
            var roleIdentityEntity = role.ToEntity();

            return await _identityRepository.CanInsertRoleAsync(roleIdentityEntity);
        }

        public async Task<List<RoleDto>> GetRolesAsync()
        {
            var roleList = await _identityRepository.GetRolesAsync();
            List<RoleDto> roleDtoList = new List<RoleDto>();

            foreach(var role in roleList)
            {
                var roleDto = role.ToModel();
                roleDtoList.Add(roleDto);
            }

            return roleDtoList;
        }

        public async Task<RoleDto> GetRoleAsync(Guid? id)
        {
            var role = await _identityRepository.GetRoleAsync(id);
            if (role == null)
            {
            }

            var roleDto = role.ToModel();
            return roleDto;
        }

        //Users
        public async Task<IdentityResult> CreateUserAsync(string email, string password)
        {
            UserDto user = new UserDto { Email = email, UserName = email, Score = 0 };
            var canInsert = await CanInsertUserAsync(user);
            if (!canInsert)
            {
                return IdentityResult.Failed();
            }

            var result = await _identityRepository.CreateUserAsync(email, password);
            return result;
        }

        public async Task<IdentityResult> DeleteUserAsync(UserDto user)
        {
            var userDto = await GetUserAsync(user.Id);

            var userIdentityEntity = userDto.ToEntity();

            return await _identityRepository.DeleteUserAsync(userIdentityEntity);
        }

        public async Task<IdentityResult> UpdateUserAsync(UserDto user)
        {
            var userIdentityEntity = user.ToEntity();

            return await _identityRepository.UpdateUserAsync(userIdentityEntity);
        }

        public async Task<bool> CanInsertUserAsync(UserDto user)
        {
            var userIdentityEntity = user.ToEntity();

            return await _identityRepository.CanInsertUserAsync(userIdentityEntity);
        }

        public async Task<List<UserDto>> GetUsersAsync()
        {
            List<UserDto> userDtoList = new List<UserDto>();
            var userList = await _identityRepository.GetUsersAsync();
            foreach(var user in userList)
            {
                var userDto = user.ToModel();
                userDtoList.Add(userDto);
            }

            return userDtoList;
        }

        public async Task<UsersDto> GetLeaderboardAsync()
        {
            var userList = await _identityRepository.GetLeaderboardAsync();
            var usersDto = userList.ToModel();

            return usersDto;
        }

        public async Task<UserDto> GetUserAsync(Guid? id)
        {
            var user = await _identityRepository.GetUserAsync(id);
            if (user == null)
            {
            }

            var userDto = user.ToModel();
            return userDto;
        }

        public async Task<UserDto> GetUserAsync(string name)
        {
            var user = await _identityRepository.GetUserAsync(name);
            if (user == null)
            {
            }

            var userDto = user.ToModel();
            return userDto;
        }

        public async Task<bool> IsUserInRoleAsync(UserDto userDto, string RoleName)
        {
            var user = userDto.ToEntity();
            return await _identityRepository.IsUserInRoleAsync(user, RoleName);
        }

        public async Task<IdentityResult> AddUserToRoleAsync(UserDto userDto, string RoleName)
        {
            var user = userDto.ToEntity();
            return await _identityRepository.AddUserToRoleAsync(user.Id, RoleName);
        }

        public async Task<IdentityResult> RemoveUserFromRoleAsync(UserDto userDto, string RoleName)
        {
            var user = userDto.ToEntity();
            return await _identityRepository.RemoveUserFromRoleAsync(user.Id, RoleName);
        }

        public async Task SignInAsync(UserDto userDto)
        {
            var userIdentity = userDto.ToEntity();

            await _identityRepository.SignInAsync(userIdentity);
        }

        public async Task<SignInResult> PasswordSignInAsync(string Email, string Password, bool RememberMe)
        {
            return await _identityRepository.PasswordSignInAsync(Email, Password, RememberMe);
        }

        public async Task SignOutAsync()
        {
            await _identityRepository.SignOutAsync();
        }

        public bool IsSignedIn(ClaimsPrincipal user)
        {
            return _identityRepository.IsSignedIn(user);
        }
    }
}
