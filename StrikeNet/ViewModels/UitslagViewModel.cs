using StrikeNet.BusinessLogic.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StrikeNet.ViewModels
{
    public class UitslagViewModel
    {
        public GameDto Game { get; set; }

        public UserDto User { get; set; }

        public bool PointsReceived { get; set; }

        public string Prediction { get; set; }

        public int EarnedPoints { get; set; }
    }
}
