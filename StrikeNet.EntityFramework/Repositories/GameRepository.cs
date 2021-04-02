using StrikeNet.EntityFramework.Entities;
using StrikeNet.EntityFramework.Interfaces;
using StrikeNet.EntityFramework.Repositories;
using StrikeNet.EntityFramework.Enums;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StrikeNet.EntityFramework.Repositories.Interface;
using StrikeNet.EntityFramework.DbContext;

namespace StrikeNet.EntityFramework.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly StrikeNetDbContext _dbContext;

        public GameRepository(StrikeNetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private bool AutoSaveChanges { get; } = true;

        public async Task<Game> GetGameAsync(int id)
        {
            return await _dbContext.Games
                .AsNoTracking()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<int> AddGameAsync(Game game)
        {
            if (game.Result != null)
            {
                game.typeResult = await CheckTypeResult(game.Result);
            }

            _dbContext.Games.Add(game);
            
            await AutoSaveChangesAsync();

            return game.Id;
        }

        public async Task<int> RemoveGameAsync(Game game)
        {
            //var predictions = await _dbContext.Predictions
            //    .AsNoTracking().Where(x => x.GameId == game.Id)
            //    .ToListAsync();

            //foreach(var prediction in predictions)
            //{
            //    _dbContext.Predictions.Remove(prediction);
            //}

            _dbContext.Games.Remove(game);

            return await AutoSaveChangesAsync();
        }

        public async Task<int> UpdateGameAsync(Game game)
        {
            if (game.Result != null)
            {
                game.typeResult = await CheckTypeResult(game.Result);
            }

            _dbContext.Games.Update(game);
            
            return await AutoSaveChangesAsync();
        }


        public async Task<bool> CanInsertGameAsync(Game game)
        {
            if (game.Id == 0)
            {
                var existsWithSameName =
                    await _dbContext.Games.Where(x => x.Team1 == game.Team1 && x.Team2 == game.Team2).SingleOrDefaultAsync();
                return existsWithSameName == null;
            }
            else
            {
                var existsWithSameName = await _dbContext.Games
                    .Where(x => x.Team1 == game.Team1 && x.Team2 == game.Team2 && x.Id != game.Id).SingleOrDefaultAsync();
                return existsWithSameName == null;
            }
        }

        public async Task<List<Game>> GetGamesAsync()
        {
            return await _dbContext.Games
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TypeResult> CheckTypeResult(string result)
        {
            TypeResult typeResult = TypeResult.NNB;
            if (result.Length == 3 && result.Substring(1, 1) == "-")
            {
                if (Convert.ToInt32(result.Substring(0, 1)) > Convert.ToInt32(result.Substring(2, 1))) { typeResult = TypeResult.WinTeam1; }
                else if (Convert.ToInt32(result.Substring(0, 1)) < Convert.ToInt32(result.Substring(2, 1))) { typeResult = TypeResult.WinTeam2; }
                else if (Convert.ToInt32(result.Substring(0, 1)) == Convert.ToInt32(result.Substring(2, 1))) { typeResult = TypeResult.Draw; }
            }
                return typeResult;
        }

        private async Task<int> AutoSaveChangesAsync()
        {
            return AutoSaveChanges ? await _dbContext.SaveChangesAsync() : (int)SavedStatus.WillBeSavedExplicitly;
        }
    }
}
