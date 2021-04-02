using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StrikeNet.EntityFramework.Interfaces;
using StrikeNet.Configuration.Constants;

namespace StrikeNet.Helpers
{
    public static class StartupHelper
    {
        public static void AddDbContexts<TStrikeNetDbContext>(this IServiceCollection services,
            IConfiguration configuration)
            where TStrikeNetDbContext : DbContext, IStrikeNetDbContext
        {
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            // UserProgression DB from existing connection
            services.AddDbContext<TStrikeNetDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString(ConfigurationConsts.StrikeNetDbConnectionStringKey),
                    sql => sql.MigrationsAssembly(migrationsAssembly)));
        }

        //public static void AddMvcExceptionFilters(this IServiceCollection services)
        //{
        //    //Exception handling
        //    services.AddScoped<ControllerExceptionFilterAttribute>();
        //}
    }
}
