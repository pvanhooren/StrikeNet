using StrikeNet.BusinessLogic.Dtos;
using StrikeNet.BusinessLogic.Mappers;
using StrikeNet.EntityFramework.Repositories.Interface;
using StrikeNet.BusinessLogic.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StrikeNet.BusinessLogic.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<int> AddGameAsync(GameDto game)
        {
            var gameEntity = game.ToEntity();

            return await _gameRepository.AddGameAsync(gameEntity);
        }

        public async Task RemoveGameAsync(GameDto game)
        {
            var gameDto = await GetGameAsync(game.Id);

            var gameEntity = gameDto.ToEntity();

            await _gameRepository.RemoveGameAsync(gameEntity);
        }

        public async Task<int> UpdateGameAsync(GameDto game)
        {
            var gameEntity = game.ToEntity();

            return await _gameRepository.UpdateGameAsync(gameEntity);
        }

        public async Task<bool> CanInsertGameAsync(GameDto game)
        {
            var gameEntity = game.ToEntity();

            return await _gameRepository.CanInsertGameAsync(gameEntity);
        }

        public async Task<GamesDto> GetGamesAsync()
        {
            var gameList = await _gameRepository.GetGamesAsync();
            var gamesDto = gameList.ToModel();

            return gamesDto;
        }

        public async Task<GameDto> GetGameAsync(int id)
        {
            var game = await _gameRepository.GetGameAsync(id);

            var gameDto = game.ToModel();
            return gameDto;
        }
    }
}
