using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.IO;

namespace StrikeNet.EntityFramework.DbContext.ContextFactories
{
    public abstract class DesignTimeDbContextFactoryBase<TContext> : IDesignTimeDbContextFactory<TContext>
        where TContext : Microsoft.EntityFrameworkCore.DbContext
    {
        protected string ConnectionString;

        public TContext CreateDbContext(string[] args)
        {
            return Create(Directory.GetCurrentDirectory(), Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT"));
        }

        protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);

        public TContext Create()
        {
            var environmentName = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
            var basePath = AppContext.BaseDirectory;
            return Create(basePath, environmentName);
        }

        public virtual TContext Create(string basePath, string environmentName)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .AddEnvironmentVariables();

            var config = builder.Build();

            ConnectionString = config.GetConnectionString("Default");

            if (string.IsNullOrWhiteSpace(ConnectionString))
                throw new InvalidOperationException(
                    "Could not find a connection string named 'Default'.");
            return Create(ConnectionString);
        }

        protected TContext Create(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException(
                    $"{nameof(connectionString)} is null or empty.",
                    nameof(connectionString));

            var optionsBuilder = new DbContextOptionsBuilder<TContext>();

            Console.WriteLine("DesignTimeDbContextFactory.Create(string): Connection string: {0}", connectionString);

            optionsBuilder.UseSqlServer(connectionString);

            var options = optionsBuilder.Options;
            return CreateNewInstance(options);
        }
    }
}
