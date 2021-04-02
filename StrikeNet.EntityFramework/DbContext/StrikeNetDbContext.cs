using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StrikeNet.EntityFramework.Constants;
using StrikeNet.EntityFramework.Entities;
using StrikeNet.EntityFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrikeNet.EntityFramework.DbContext
{
    public class StrikeNetDbContext : IdentityDbContext<UserIdentity, RoleIdentity, Guid>, IStrikeNetDbContext
    {
        public StrikeNetDbContext(DbContextOptions<StrikeNetDbContext> options)
           : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }

        public DbSet<Prediction> Predictions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureStrikeNetContext(modelBuilder);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Game>().HasData(
                new Game
                {
                    Id = 1,
                    Team1 = "RB Leipzig",
                    Team2 = "Tottenham Hotspur",
                    Result = "3-0",
                    Date = new DateTime(2020, 3, 10, 21, 0, 0),
                    typeResult = Enums.TypeResult.WinTeam1
                }) ;
        }

        private void ConfigureStrikeNetContext(ModelBuilder builder)
        {
            builder.Entity<Game>().ToTable(TableConsts.Games);
            builder.Entity<Prediction>().ToTable(TableConsts.Predictions);
        }
    }
}
