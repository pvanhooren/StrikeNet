using StrikeNet.EntityFramework.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrikeNet.BusinessLogic.Dtos
{
    public class PredictionDto
    {
        public int Id { get; set; }
        public int GameId { get; set; }

        public Guid? UserId { get; set; }

        public string PredictedResult { get; set; }


        public TypeResult PredictedTypeResult { get; set; }

        public int EarnedPoints { get; set; }

        public bool PointsReceived { get; set; }
    }
}
