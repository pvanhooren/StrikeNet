using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StrikeNet.BusinessLogic.Dtos;
using StrikeNet.BusinessLogic.Services.Interface;
using StrikeNet.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StrikeNet.Controllers
{
    [Authorize]
    public class VoorspellingenController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IPredictionService _predictionService;
        private readonly IIdentityService _identityService;

        public VoorspellingenController(IGameService gameService, IPredictionService predictionService, IIdentityService identityService)
        {
            _gameService = gameService;
            _predictionService = predictionService;
            _identityService = identityService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Voorspellingen()
        {
            return View(await _gameService.GetGamesAsync()); ;
        }

        [HttpGet]
        public async Task<IActionResult> Voorspelling(int gameId, int userId)
        {
            bool hasPredicted;
            string predictionString;
            var game = await _gameService.GetGameAsync(gameId);
            var userClaims = HttpContext.User;
            var userName = userClaims.Identity.Name;
            var user = await _identityService.GetUserAsync(userName);
            var prediction = await _predictionService.GetPredictionAsync(gameId, user.Id);

            if(prediction == null)
            {
                hasPredicted = false;
                predictionString = "";
            } else
            {
                hasPredicted = true;
                predictionString = prediction.PredictedResult;
            }
            return View(new VoorspellingViewModel { Game = game, User = user, Prediction = predictionString, HasPredicted = hasPredicted });
        }

        [HttpPost]
        public async Task<IActionResult> Voorspelling(VoorspellingViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.HasPredicted == false)
                {
                    await _predictionService.SavePredictionAsync(model.Game.Id, model.User.Id, model.Prediction);
                } else
                {
                    await _predictionService.UpdatePredictionAsync(model.Game.Id, model.User.Id, model.Prediction);
                }
                return RedirectToAction(nameof(Voorspellingen));
            }

            return View(model);
        }
    }
}
