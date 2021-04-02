using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrikeNet.EntityFramework.DbContext.ContextFactories
{
    public class StrikeNetDbContextFactory : DesignTimeDbContextFactoryBase<StrikeNetDbContext>
    {
        protected override StrikeNetDbContext CreateNewInstance(DbContextOptions<StrikeNetDbContext> options)
        {
            return new StrikeNetDbContext(options);
        }

        public override StrikeNetDbContext Create(string basePath, string environmentName)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .AddEnvironmentVariables();

            var config = builder.Build();

            ConnectionString = config.GetConnectionString("StrikeNetDbConnection");

            if (string.IsNullOrWhiteSpace(ConnectionString))
                throw new InvalidOperationException(
                    "Could not find a connection string named 'Default'.");
            return Create(ConnectionString);
        }
    }
}
