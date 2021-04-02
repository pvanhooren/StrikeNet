using AutoMapper;
using StrikeNet.BusinessLogic.Dtos;
using StrikeNet.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrikeNet.BusinessLogic.Mappers
{
    public static class PredictionMappers
    {
        static PredictionMappers()
        {
            Mapper = new MapperConfiguration(cfg => { cfg.AddProfile<PredictionMapperProfile>(); }).CreateMapper();
        }

        internal static IMapper Mapper { get; }

        public static Prediction ToEntity(this PredictionDto prediction)
        {
            return prediction == null ? null : Mapper.Map<Prediction>(prediction);
        }

        public static PredictionDto ToModel(this Prediction prediction)
        {
            return prediction == null ? null : Mapper.Map<PredictionDto>(prediction);
        }
    }
}
