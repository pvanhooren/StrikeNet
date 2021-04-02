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
    public class PredictionRepository : IPredictionRepository
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly StrikeNetDbContext _dbContext;

        public PredictionRepository(IIdentityRepository identityRepository, StrikeNetDbContext dbContext)
        {
            _identityRepository = identityRepository;
            _dbContext = dbContext;
        }

        private bool AutoSaveChanges { get; } = true;

        public async Task<Prediction> GetPredictionAsync(int gameId, Guid? userId)
        {
            var prediction = await _dbContext.Predictions
            .AsNoTracking()
            .Where(x => x.GameId == gameId && x.UserId == userId)
            .FirstOrDefaultAsync();
            return prediction;
        }

        public async Task SavePredictionAsync(int gameId, Guid? userId, string predictionString)
        {
            TypeResult typeResult = await CheckTypeResult(predictionString);
            Prediction prediction = new Prediction { GameId = gameId, UserId = userId, PredictedResult = predictionString, PredictedTypeResult = typeResult };
            _dbContext.Predictions.Add(prediction);
            await AutoSaveChangesAsync();
        }

        public async Task UpdatePredictionAsync(Prediction predictionEntity)
        {
            //Prediction prediction = await GetPredictionAsync(predictionEntity.GameId, predictionEntity.UserId);
            TypeResult typeResult = await CheckTypeResult(predictionEntity.PredictedResult);
            predictionEntity.PredictedTypeResult = typeResult;
            //prediction.PredictedResult = predictionEntity.;
            _dbContext.Predictions.Update(predictionEntity);
            await AutoSaveChangesAsync();
        }

        public async Task<int> CompareResultsAsync(Game game, Prediction prediction)
        {
            if(game.typeResult == TypeResult.NNB)
            {
                game.typeResult = await CheckTypeResult(game.Result);
            }

            if(prediction.PredictedTypeResult == TypeResult.NNB)
            {
                prediction.PredictedTypeResult = await CheckTypeResult(prediction.PredictedResult);
            }

            int EarnedPoints = 0;

            if (prediction.PredictedResult == game.Result)
            {
                EarnedPoints = 3;
                prediction.EarnedPoints = 3;
            } else if(prediction.PredictedTypeResult == game.typeResult)
            {
                EarnedPoints = 1;
                prediction.EarnedPoints = 1;
            } else
            {
                prediction.EarnedPoints = 0;
            }
            return EarnedPoints;
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

        public async Task AddPointsAsync(Prediction prediction)
        {
            var user = await _identityRepository.GetUserAsync(prediction.UserId);
            prediction.PointsReceived = true;
            _dbContext.Predictions.Update(prediction);
            await _dbContext.SaveChangesAsync();
            user.Score += prediction.EarnedPoints;
            await _identityRepository.UpdateUserAsync(user);
        }

        private async Task<int> AutoSaveChangesAsync()
        {
            return AutoSaveChanges ? await _dbContext.SaveChangesAsync() : (int)SavedStatus.WillBeSavedExplicitly;
        }
    }
}
