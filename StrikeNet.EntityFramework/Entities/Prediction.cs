using StrikeNet.EntityFramework.DbContext;
using StrikeNet.EntityFramework.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StrikeNet.EntityFramework.Entities
{
    public class Prediction
    {
        [Key] public int Id { get; set; }

        public int GameId { get; set; }

        public Guid? UserId { get; set; }

        public string PredictedResult { get; set; }

        public TypeResult PredictedTypeResult { get; set; }

        public int EarnedPoints { get; set; }

        public bool PointsReceived { get; set; }
    }
}
