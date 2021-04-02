using StrikeNet.BusinessLogic.Dtos;
using StrikeNet.EntityFramework.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StrikeNet.BusinessLogic.Services.Interface
{
    public interface IPredictionService
    {
        Task<PredictionDto> GetPredictionAsync(int gameId, Guid? userId);

        Task SavePredictionAsync(int gameId, Guid? userId, string Prediction);

        Task UpdatePredictionAsync(int gameId, Guid? userId, string Prediction);

        Task<int> CompareResultsAsync(GameDto game, PredictionDto prediction);

        Task AddPointsAsync(Guid? userId, int gameId, int Points);

        Task<TypeResult> CheckTypeResult(string Result);
    }
}
