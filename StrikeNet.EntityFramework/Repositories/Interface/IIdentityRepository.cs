using Microsoft.AspNetCore.Identity;
using StrikeNet.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StrikeNet.EntityFramework.Repositories.Interface
{
    public interface IIdentityRepository
    {
        //Roles
        Task<bool> ExistsRoleAsync(Guid? roleId);

        Task<List<RoleIdentity>> GetRolesAsync();

        Task<RoleIdentity> GetRoleAsync(Guid? roleId);

        Task<bool> CanInsertRoleAsync(RoleIdentity role);

        Task<IdentityResult> UpdateRoleAsync(RoleIdentity role);

        Task<IdentityResult> DeleteRoleAsync(RoleIdentity role);

        Task<(IdentityResult identityResult, Guid? roleId)> CreateRoleAsync(RoleIdentity role);


        //Users
        Task<bool> ExistsUserAsync(Guid? userId);

        Task<List<UserIdentity>> GetUsersAsync();

        Task<List<UserIdentity>> GetLeaderboardAsync();

        Task<UserIdentity> GetUserAsync(Guid? userId);

        Task<UserIdentity> GetUserAsync(string userName);

        Task<bool> CanInsertUserAsync(UserIdentity user);

        Task<IdentityResult> UpdateUserAsync(UserIdentity user);

        Task<IdentityResult> DeleteUserAsync(UserIdentity user);

        Task<IdentityResult> CreateUserAsync(string email, string password);

        Task<bool> IsUserInRoleAsync(UserIdentity user, string RoleName);

        Task<IdentityResult> AddUserToRoleAsync(Guid? userId, string RoleName);

        Task<IdentityResult> RemoveUserFromRoleAsync(Guid? userId, string RoleName);

        Task SignInAsync(UserIdentity user);

        Task<SignInResult> PasswordSignInAsync(string Email, string Password, bool RememberMe);

        Task SignOutAsync();

        bool IsSignedIn(ClaimsPrincipal user);
    }
}
