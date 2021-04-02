using AutoMapper;
using StrikeNet.BusinessLogic.Dtos;
using StrikeNet.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrikeNet.BusinessLogic.Mappers
{
    public class GameMapperProfile : Profile
    {

        public GameMapperProfile()
        {
            CreateMap<Game, GameDto>(MemberList.Destination);

            CreateMap<List<Game>, GamesDto>(MemberList.Destination)
                .ForMember(x => x.AllGames,
                    opt => opt.MapFrom(src => src));

            // model to entity
            CreateMap<GameDto, Game>(MemberList.Source);
        }
    }
}
