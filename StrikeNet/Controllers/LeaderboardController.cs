using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StrikeNet.BusinessLogic.Services.Interface;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StrikeNet.Controllers
{
    [Authorize]
    public class LeaderboardController : Controller
    {
        private readonly IIdentityService _identityService;

        public LeaderboardController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpGet]
        public async Task<IActionResult> Leaderboards()
        {
            return View(await _identityService.GetLeaderboardAsync());
        }
    }
}
