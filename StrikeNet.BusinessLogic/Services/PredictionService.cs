using StrikeNet.BusinessLogic.Dtos;
using StrikeNet.BusinessLogic.Mappers;
using StrikeNet.BusinessLogic.Services.Interface;
using StrikeNet.EntityFramework.Enums;
using StrikeNet.EntityFramework.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StrikeNet.BusinessLogic.Services
{
    public class PredictionService : IPredictionService
    {
        private readonly IPredictionRepository _predictionRepository;
        
        public PredictionService(IPredictionRepository predictionRepository)
        {
            _predictionRepository = predictionRepository;
        }

        public async Task<PredictionDto> GetPredictionAsync(int gameId, Guid? userId)
        {
            var prediction = await _predictionRepository.GetPredictionAsync(gameId, userId);
            return prediction.ToModel();
        }

        public async Task SavePredictionAsync(int gameId, Guid? userId, string prediction)
        {
            await _predictionRepository.SavePredictionAsync(gameId, userId, prediction);
        }

        public async Task UpdatePredictionAsync(int gameId, Guid? userId, string prediction)
        {
            PredictionDto predictionDto = new PredictionDto()
            {
                GameId = gameId,
                UserId = userId,
                PredictedResult = prediction
            };

            var predictionEntity = predictionDto.ToEntity();

            await _predictionRepository.UpdatePredictionAsync(predictionEntity);
        }

        public async Task<int> CompareResultsAsync(GameDto game, PredictionDto prediction)
        {
            var gameEntity = game.ToEntity();
            var predictionEntity = prediction.ToEntity();
            var test= await _predictionRepository.CompareResultsAsync(gameEntity, predictionEntity);
            return test;
        }

        public async Task AddPointsAsync(Guid? userId, int gameId, int points)
        {
            var prediction = await _predictionRepository.GetPredictionAsync(gameId, userId);

            if (prediction != null)
            {
                prediction.EarnedPoints = points;
            } else
            {
                PredictionDto predictionDto = new PredictionDto()
                {
                    GameId = gameId,
                    UserId = userId,
                    EarnedPoints = points
                };

                prediction = predictionDto.ToEntity();
            }

            await _predictionRepository.AddPointsAsync(prediction);
        }

        public async Task<TypeResult> CheckTypeResult(string result)
        {
            return await _predictionRepository.CheckTypeResult(result);
        }
    }
}
