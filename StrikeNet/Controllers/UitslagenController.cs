using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StrikeNet.BusinessLogic.Services.Interface;
using StrikeNet.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StrikeNet.Controllers
{
    [Authorize]
    public class UitslagenController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IPredictionService _predictionService;
        private readonly IIdentityService _identityService;

        public UitslagenController(IGameService gameService, IPredictionService predictionService, IIdentityService identityService)
        {
            _gameService = gameService;
            _predictionService = predictionService;
            _identityService = identityService;
        }

        [HttpGet]
        public async Task<IActionResult> Uitslagen()
        {
            return View(await _gameService.GetGamesAsync()); ;
        }

        [HttpGet]
        public async Task<IActionResult> Uitslag(int gameId)
        {
            string predictionString;
            int pointsEarned;
            bool pointsReceived;
            var game = await _gameService.GetGameAsync(gameId);
            var userClaims = HttpContext.User;
            var userName = userClaims.Identity.Name;
            var user = await _identityService.GetUserAsync(userName);
            var prediction = await _predictionService.GetPredictionAsync(gameId, user.Id);

            if (prediction == null)
            {
                predictionString = "";
                pointsEarned = 0;
                pointsReceived = true;
            }
            else
            {
                pointsEarned = await _predictionService.CompareResultsAsync(game, prediction); 
                predictionString = prediction.PredictedResult;
                pointsReceived = prediction.PointsReceived;
            }

            return View(new UitslagViewModel { Game = game, User = user, Prediction = predictionString, PointsReceived = pointsReceived, EarnedPoints = pointsEarned });
        }

        [HttpPost]
        public async Task<IActionResult> Uitslag(UitslagViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.PointsReceived == false)
                {
                    await _predictionService.AddPointsAsync(model.User.Id, model.Game.Id, model.EarnedPoints);
                    return RedirectToAction(nameof(Uitslagen));
                }
            }
            return View(model);
        }
    }
}
