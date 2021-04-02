using StrikeNet.EntityFramework.Entities;
using StrikeNet.EntityFramework.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StrikeNet.EntityFramework.Repositories.Interface
{
    public interface IPredictionRepository
    {
        Task<Prediction> GetPredictionAsync(int gameId, Guid? userId);

        Task SavePredictionAsync(int gameId, Guid? userId, string prediction);

        Task UpdatePredictionAsync(Prediction predictionEntity);

        Task<int> CompareResultsAsync(Game game, Prediction prediction);

        Task AddPointsAsync(Prediction prediction);

        Task<TypeResult> CheckTypeResult(string result);
    }
}
