using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using StrikeNet.BusinessLogic.Dtos;
using StrikeNet.EntityFramework.Entities;

namespace StrikeNet.BusinessLogic.Mappers
{
    public static class GameMappers
    {
        static GameMappers()
        {
            Mapper = new MapperConfiguration(cfg => { cfg.AddProfile<GameMapperProfile>(); }).CreateMapper();
        }

        internal static IMapper Mapper { get; }

        public static Game ToEntity(this GameDto game)
        {
            return game == null ? null : Mapper.Map<Game>(game);
        }

        public static GameDto ToModel(this Game game)
        {
            return game == null ? null : Mapper.Map<GameDto>(game);
        }

        public static GamesDto ToModel(this List<Game> games)
        {
            return games == null ? null : Mapper.Map<GamesDto>(games);
        }
    }
}
