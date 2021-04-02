using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StrikeNet.BusinessLogic.Dtos;
using StrikeNet.BusinessLogic.Services.Interface;
using StrikeNet.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StrikeNet.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {
        private readonly IIdentityService _identityService;

        public AdministrationController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
            RoleDto roleDto = new RoleDto
            {
                 Name = model.RoleName
            };
            Guid? roleId = await _identityService.CreateRoleAsync(roleDto);
            if (roleId != null)
            {
                return RedirectToAction("ListRoles", "Administration");
            }
            }

            ViewBag.ErrorMessage = $"Rol aanmaken mislukt, probeer het opnieuw.";
            return View("NotFound");
        }

        [HttpGet]
        public async Task<IActionResult> ListRoles()
        {
            var roles = await _identityService.GetRolesAsync();
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(Guid? id)
        {
            var role = await _identityService.GetRoleAsync(id);

            if(role == null)
            {
                ViewBag.ErrorMessage = $"Rol met id {id} bestaat niet";
                return View("NotFound");
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name,
            };

            var Users = await _identityService.GetUsersAsync();

            foreach(var user in Users)
            {
                if(await _identityService.IsUserInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await _identityService.GetRoleAsync(model.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Rol met id {model.Id} bestaat niet";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await _identityService.UpdateRoleAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(Guid? roleId)
        {
            ViewBag.roleId = roleId;
            var role = await _identityService.GetRoleAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Rol met id {roleId} bestaat niet";
                return View("NotFound");
            }

            var model = new List<UserRoleViewModel>();

            var Users = await _identityService.GetUsersAsync();

            foreach (var user in Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await _identityService.IsUserInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, Guid? roleId)
        {
            var role = await _identityService.GetRoleAsync(roleId);

            if(role == null)
            {
                ViewBag.ErrorMessage = $"Rol met id {roleId} bestaat niet";
                return View("NotFound");
            }

            for(int i = 0; i<model.Count; i++)
            {
                var user = await _identityService.GetUserAsync(model[i].UserId);
                IdentityResult result = null;

                if (model[i].IsSelected && !await _identityService.IsUserInRoleAsync(user, role.Name))
                {
                    result = await _identityService.AddUserToRoleAsync(user, role.Name);
                } else if (!model[i].IsSelected && await _identityService.IsUserInRoleAsync(user, role.Name))
                {
                    result = await _identityService.RemoveUserFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if(i<(model.Count - 1))
                    {
                        continue;
                    }
                    else
                    {
                        return RedirectToAction("EditRole", new { Id = roleId });
                    }
                }
            }
            return RedirectToAction("EditRole", new { Id = roleId });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveRole(Guid? id)
        {
            if (id == null) return NotFound();

            var roleDto = await _identityService.GetRoleAsync(id);
            return View(roleDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveRole(RoleDto roleDto)
        {
            await _identityService.DeleteRoleAsync(roleDto);
            return RedirectToAction(nameof(ListRoles));
        }
    }
}
