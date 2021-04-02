using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StrikeNet.BusinessLogic.Dtos;

namespace StrikeNet.BusinessLogic.Services.Interface
{
    public interface IGameService
    {
        Task<int> AddGameAsync(GameDto game);

        Task RemoveGameAsync(GameDto game);

        Task<int> UpdateGameAsync(GameDto game);

        Task<bool> CanInsertGameAsync(GameDto game);

        Task<GamesDto> GetGamesAsync();

        Task<GameDto> GetGameAsync(int id);
    }
}
