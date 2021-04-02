using Microsoft.AspNetCore.Identity;
using StrikeNet.BusinessLogic.Dtos;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StrikeNet.BusinessLogic.Services.Interface
{
    public interface IIdentityService
    {
        //Roles
        Task<bool> ExistsRoleAsync(Guid? roleId);

        Task<Guid?> CreateRoleAsync(RoleDto role);

        Task<IdentityResult> DeleteRoleAsync(RoleDto role);

        Task<IdentityResult> UpdateRoleAsync(RoleDto role);

        Task<bool> CanInsertRoleAsync(RoleDto role);

        Task<List<RoleDto>> GetRolesAsync();

        Task<RoleDto> GetRoleAsync(Guid? id);


        //Users
        Task<bool> ExistsUserAsync(Guid? userId);

        Task<IdentityResult> CreateUserAsync(string Email, string Password);

        Task<IdentityResult> DeleteUserAsync(UserDto user);

        Task<IdentityResult> UpdateUserAsync(UserDto user);

        Task<bool> CanInsertUserAsync(UserDto user);

        Task<List<UserDto>> GetUsersAsync();

        Task<UsersDto> GetLeaderboardAsync();

        Task<UserDto> GetUserAsync(Guid? id);

        Task<UserDto> GetUserAsync(string name);

        Task<bool> IsUserInRoleAsync(UserDto userDto, string RoleName);

        Task<IdentityResult> AddUserToRoleAsync(UserDto userDto, string RoleName);

        Task<IdentityResult> RemoveUserFromRoleAsync(UserDto userDto, string RoleName);

        Task SignInAsync(UserDto userDto);

        Task<SignInResult> PasswordSignInAsync(string Email, string Password, bool RememberMe);

        Task SignOutAsync();

        bool IsSignedIn(ClaimsPrincipal user);
    }
}
