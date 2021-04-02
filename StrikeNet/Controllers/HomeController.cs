using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StrikeNet.BusinessLogic.Services.Interface;
using StrikeNet.ViewModels;

namespace StrikeNet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGameService _gameService;
        private readonly IPredictionService _predictionService;
        private readonly IIdentityService _identityService;

        public HomeController(IGameService gameService, IPredictionService predictionService, IIdentityService identityService)
        {
            _gameService = gameService;
            _predictionService = predictionService;
            _identityService = identityService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomePageViewModel();
            var games = await _gameService.GetGamesAsync();
            var users = await _identityService.GetLeaderboardAsync();
            model.Games = games.AllGames;
            model.Users = users.Results;
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
