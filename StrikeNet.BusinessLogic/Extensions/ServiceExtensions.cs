using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StrikeNet.BusinessLogic.Services;
using StrikeNet.BusinessLogic.Services.Interface;
using StrikeNet.EntityFramework.Interfaces;
using StrikeNet.EntityFramework.Repositories;
using StrikeNet.EntityFramework.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrikeNet.BusinessLogic.Extensions
{
    public static class ServiceExtensions
    { 
        public static IServiceCollection AddStrikeNetServices<StrikeNetDbContext>(this IServiceCollection services)
            where StrikeNetDbContext : DbContext, IStrikeNetDbContext
        {
            services.AddScoped<IGameRepository, GameRepository>();

            services.AddScoped<IGameService, GameService>();

            return services;
        }
    }
}
