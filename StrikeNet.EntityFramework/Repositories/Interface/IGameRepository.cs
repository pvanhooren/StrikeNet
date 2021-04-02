using StrikeNet.EntityFramework.Entities;
using StrikeNet.EntityFramework.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StrikeNet.EntityFramework.Repositories.Interface
{
    public interface IGameRepository
    {
        Task<int> AddGameAsync(Game game);

        Task<int> RemoveGameAsync(Game game);

        Task<int> UpdateGameAsync(Game game);

        Task<bool> CanInsertGameAsync(Game game);

        Task<List<Game>> GetGamesAsync();

        Task<Game> GetGameAsync(int id);

        Task<TypeResult> CheckTypeResult(string result);
    }
}
