using AutoMapper;
using StrikeNet.BusinessLogic.Dtos;
using StrikeNet.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrikeNet.BusinessLogic.Mappers
{
    public class PredictionMapperProfile : Profile
    {

        public PredictionMapperProfile()
        {
            CreateMap<Prediction, PredictionDto>(MemberList.Destination);


            // model to entity
            CreateMap<PredictionDto, Prediction>(MemberList.Source);
        }
    }
}
