using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StrikeNet.EntityFramework.Entities;
using StrikeNet.EntityFramework.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StrikeNet.EntityFramework.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly UserManager<UserIdentity> _userManager;
        private readonly RoleManager<RoleIdentity> _roleManager;
        private readonly SignInManager<UserIdentity> _signInManager;

        public IdentityRepository(UserManager<UserIdentity> userManager,
            RoleManager<RoleIdentity> roleManager, SignInManager<UserIdentity> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public virtual Task<bool> ExistsUserAsync(Guid? userId)
        {
            return _userManager.Users.AnyAsync(x => x.Id.Equals(userId));
        }

        public virtual Task<bool> ExistsRoleAsync(Guid? roleId)
        {
            return _roleManager.Roles.AnyAsync(x => x.Id.Equals(roleId));
        }

        public async Task<List<UserIdentity>> GetUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<List<UserIdentity>> GetLeaderboardAsync()
        {
            return await _userManager.Users.OrderByDescending(x => x.Score).ToListAsync();
        }

        public async Task<List<RoleIdentity>> GetRolesAsync()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<UserIdentity> GetUserAsync(Guid? userId)
        {
            return await _userManager.Users.Where(x => x.Id.Equals(userId)).SingleOrDefaultAsync();
        }

        public async Task<UserIdentity> GetUserAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<RoleIdentity> GetRoleAsync(Guid? roleId)
        {
            return await _roleManager.Roles.Where(x => x.Id.Equals(roleId)).SingleOrDefaultAsync();
        }

        public async Task<(IdentityResult identityResult, Guid? roleId)> CreateRoleAsync(RoleIdentity role)
        {
            var identityResult = await _roleManager.CreateAsync(role);

            return (identityResult, role.Id);
        }

        public async Task<IdentityResult> DeleteRoleAsync(RoleIdentity role)
        {
            var thisRole = await _roleManager.FindByIdAsync(role.Id.ToString());
            return await _roleManager.DeleteAsync(thisRole);

        }

        public async Task<IdentityResult> UpdateRoleAsync(RoleIdentity role)
        {
            var thisRole = await _roleManager.FindByIdAsync(role.Id.ToString());
            thisRole.Name = role.Name;
            return await _roleManager.UpdateAsync(thisRole);
        }

        public async Task<bool> CanInsertRoleAsync(RoleIdentity role)
        {
            var existsWithSameName =
                await _roleManager.Roles.Where(x => x.Name == role.Name).SingleOrDefaultAsync();
            return existsWithSameName == null;
        }

        public async Task<bool> CanInsertUserAsync(UserIdentity user)
        {
            var existsWithSameName =
                await _userManager.Users.Where(x => x.Email == user.Email).SingleOrDefaultAsync();
            return existsWithSameName == null;
        }

        public async Task<IdentityResult> CreateUserAsync(string email, string password)
        {
            UserIdentity user = new UserIdentity { Email = email, UserName = email, Score = 0 };
            var identityResult = await _userManager.CreateAsync(user, password);

            return identityResult;
        }

        public async Task<IdentityResult> DeleteUserAsync(UserIdentity user)
        {
            var thisUser = await _userManager.FindByIdAsync(user.Id.ToString());
            return await _userManager.DeleteAsync(thisUser);
        }

        public async Task<IdentityResult> UpdateUserAsync(UserIdentity user)
        {
            var thisUser = await _userManager.FindByIdAsync(user.Id.ToString());
            thisUser.UserName = user.UserName;
            thisUser.Email = user.Email;
            thisUser.Score = user.Score;
            thisUser.EmailConfirmed = user.EmailConfirmed;
            thisUser.PhoneNumber = user.PhoneNumber;
            thisUser.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
            thisUser.LockoutEnabled = user.LockoutEnabled;
            thisUser.LockoutEnabled = user.LockoutEnabled;
            thisUser.TwoFactorEnabled = user.TwoFactorEnabled;
            thisUser.AccessFailedCount = user.AccessFailedCount;
            thisUser.LockoutEnd = user.LockoutEnd;
            return await _userManager.UpdateAsync(thisUser);
        }

        public async Task<bool> IsUserInRoleAsync(UserIdentity user, string RoleName)
        {
            return await _userManager.IsInRoleAsync(user, RoleName);
        }

        public async Task<IdentityResult> AddUserToRoleAsync(Guid? userId, string RoleName)
        {
            var user = await GetUserAsync(userId);
            return await _userManager.AddToRoleAsync(user, RoleName);
        }

        public async Task<IdentityResult> RemoveUserFromRoleAsync(Guid? userId, string RoleName)
        {
            var user = await GetUserAsync(userId);
            return await _userManager.RemoveFromRoleAsync(user, RoleName);
        }

        public async Task SignInAsync(UserIdentity user)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
        }

        public async Task<SignInResult> PasswordSignInAsync(string Email, string Password, bool RememberMe)
        {
            return await _signInManager.PasswordSignInAsync(Email, Password, RememberMe, false);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public bool IsSignedIn(ClaimsPrincipal user)
        {
            return _signInManager.IsSignedIn(user);
        }
    }
}
