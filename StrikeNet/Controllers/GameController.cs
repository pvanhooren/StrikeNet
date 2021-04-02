using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StrikeNet.BusinessLogic.Dtos;
using StrikeNet.BusinessLogic.Services.Interface;
using StrikeNet.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StrikeNet.Areas.Admin.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IPredictionService _predictionService;
        private readonly IIdentityService _identityService;

        public GameController(IGameService gameService, IPredictionService predictionService, IIdentityService identityService)
        {
            _gameService = gameService;
            _predictionService = predictionService;
            _identityService = identityService;
        }

        [HttpGet]
        public async Task<IActionResult> Games()
        {
            return View(await _gameService.GetGamesAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Game(int id)
        {
            var gameDto = await _gameService.GetGameAsync(id);
            return View(gameDto);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateGame()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateGame(GameViewModel model)
        {
            if (ModelState.IsValid)
            {
                GameDto gameDto = new GameDto();
                gameDto.Date = model.Date;
                gameDto.Id = model.Id;
                gameDto.Result = model.Result;
                gameDto.Team1 = model.Team1;
                gameDto.Team2 = model.Team2;
                var id = await _gameService.AddGameAsync(gameDto);
                return RedirectToAction(nameof(Games));
            } else
            {
                return View(model);
            }

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveGame(int id)
        {
            if (id == 0) return NotFound();

            var gameDto = await _gameService.GetGameAsync(id);
            return View(gameDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveGame(GameDto gameDto)
        {
            await _gameService.RemoveGameAsync(gameDto);
            return RedirectToAction(nameof(Games));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateGame(int id)
        {
            if (id == 0) return NotFound();

            var gameDto = await _gameService.GetGameAsync(id);

            if (gameDto != null) {
                GameViewModel model = new GameViewModel();
                model.Id = id;
                model.Team1 = gameDto.Team1;
                model.Team2 = gameDto.Team2;
                model.Date = gameDto.Date;
                model.Result = gameDto.Result;
            return View(model);
            } else
            {
                return View();
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateGame(GameViewModel model)
        {
            if (ModelState.IsValid)
            {
                GameDto gameDto = new GameDto();
                gameDto.Date = model.Date;
                gameDto.Id = model.Id;
                gameDto.Result = model.Result;
                gameDto.Team1 = model.Team1;
                gameDto.Team2 = model.Team2;
                await _gameService.UpdateGameAsync(gameDto);
                return RedirectToAction(nameof(Games));
            } else
            {
                return View(model);
            }
        }
    }
}
