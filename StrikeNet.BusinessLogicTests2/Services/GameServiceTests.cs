using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StrikeNet.BusinessLogic.Dtos;
using StrikeNet.BusinessLogic.Services;
using StrikeNet.EntityFramework.DbContext;
using StrikeNet.EntityFramework.Entities;
using StrikeNet.EntityFramework.Enums;
using StrikeNet.EntityFramework.Interfaces;
using StrikeNet.EntityFramework.Repositories;
using StrikeNet.EntityFramework.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrikeNet.BusinessLogic.Services.Tests
{
    [TestClass()]
    public class GameServiceTests
    {
        private GameService _gameService;

        public async Task CreateDb()
        {
            var options = new DbContextOptionsBuilder<StrikeNetDbContext>()
                .UseInMemoryDatabase(databaseName: "StrikeNetTestDb")
                .EnableSensitiveDataLogging()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;

            var dbContext = new StrikeNetDbContext(options);

            if (await dbContext.Games.CountAsync() <= 0)
            {
                Game g1 = new Game()
                {
                    Id = 1,
                    Team1 = "FCB",
                    Team2 = "BVB",
                    Date = DateTime.UtcNow
                };
                Game g2 = new Game()
                {
                    Id = 2,
                    Team1 = "PSG",
                    Team2 = "Manchester City",
                    Date = DateTime.UtcNow
                };
                Game g3 = new Game()
                {
                    Id = 3,
                    Team1 = "Ajax",
                    Team2 = "Feyenoord",
                    Date = DateTime.UtcNow
                };
                Game g4 = new Game()
                {
                    Id = 4,
                    Team1 = "Liverpool",
                    Team2 = "Real Madrid",
                    Date = DateTime.UtcNow
                };
                dbContext.Games.Add(g1);
                dbContext.Games.Add(g2);
                dbContext.Games.Add(g3);
                dbContext.Games.Add(g4);
                await dbContext.SaveChangesAsync();
            }

            var gameRepository = new GameRepository(dbContext);
            _gameService = new GameService(gameRepository);
        }

        [TestMethod()]
        public async Task AddGameAsyncTest()
        {
            //Arrange
            await CreateDb();

            GameDto game = new GameDto()
            {
                Team1 = "Juve",
                Team2 = "Real",
                Date = DateTime.UtcNow
            };

            //Act
            var id = await _gameService.AddGameAsync(game);
            var expected = await _gameService.GetGameAsync(id);

            //Assert
            Assert.AreEqual("Juve", expected.Team1);
        }

        [TestMethod()]
        public async Task AddGameAsyncTest1()
        {
            //Arrange
            await CreateDb();

            GameDto game = new GameDto()
            {
                Team1 = "Napoli",
                Team2 = "Brugge",
                Result = "0-0",
                Date = DateTime.UtcNow
            };

            //Act
            var id = await _gameService.AddGameAsync(game);
            var expected = await _gameService.GetGameAsync(id);

            //Assert
            Assert.AreEqual("Napoli", expected.Team1);
        }

        [TestMethod()]
        public async Task CanInsertGameAsyncTest()
        {
            //Arrange
            await CreateDb();

            GameDto game = new GameDto()
            {
                Team1 = "FCB",
                Team2 = "BVB",
                Date = DateTime.UtcNow
            };

            //Act
            var able = await _gameService.CanInsertGameAsync(game);

            //Assert
            Assert.AreEqual(false, able);
        }

        [TestMethod()]
        public async Task CanInsertGameAsyncTest1()
        {
            //Arrange
            await CreateDb();

            GameDto game = new GameDto()
            {
                Team1 = "Zenit",
                Team2 = "Bayern",
                Date = DateTime.UtcNow
            };

            //Act
            var able = await _gameService.CanInsertGameAsync(game);

            //Assert
            Assert.AreEqual(true, able);
        }

        [TestMethod()]
        public async Task CanInsertGameAsyncTest2()
        {
            //Arrange
            await CreateDb();

            GameDto game = new GameDto()
            {
                Id = 2,
                Team1 = "FCB",
                Team2 = "BVB",
                Date = DateTime.UtcNow
            };

            //Act
            var able = await _gameService.CanInsertGameAsync(game);

            //Assert
            Assert.AreEqual(false, able);
        }

        [TestMethod()]
        public async Task GetGameAsyncTest()
        {
            //Arrange 
            await CreateDb();

            //Act
            var game = await _gameService.GetGameAsync(3);

            //Assert
            Assert.AreEqual("Ajax", game.Team1);
        }

        [TestMethod()]
        public async Task GetGamesAsyncTest()
        {
            //Arrange
            await CreateDb();

            //Act
            var games = await _gameService.GetGamesAsync();

            //Assert
            Assert.AreEqual(6, games.AllGames.Count);
        }

        [TestMethod()]
        public async Task RemoveGameAsyncTest()
        {
            //Arrange
            await CreateDb();

            GameDto gameDto = new GameDto()
            {
                Id = 2,
                Team1 = "PSG",
                Team2 = "Manchester City",
                Date = DateTime.UtcNow
            };

            //Act
            await _gameService.RemoveGameAsync(gameDto);
            var games = await _gameService.GetGamesAsync();

            //Assert
            Assert.AreEqual(5, games.AllGames.Count);
        }

        [TestMethod()]
        public async Task UpdateGameAsyncTest()
        {
            //Arrange
            await CreateDb();

            GameDto gameDto = new GameDto()
            {
                Id = 1,
                Team1 = "FCB",
                Team2 = "AZ",
                Result = "0-2",
                Date = DateTime.UtcNow
            };

            //Act
            await _gameService.UpdateGameAsync(gameDto);
            var game = await _gameService.GetGameAsync(1);

            //Assert
            Assert.AreEqual("AZ", game.Team2);
        }
            
        [TestMethod()]
        public async Task UpdateGameAsyncTest1()
        {
            //Arrange
            await CreateDb();

            GameDto gameDto = new GameDto()
            {
                Id = 3,
                Team1 = "Ajax",
                Team2 = "Feyenoord",
                Result = "8-0",
                Date = DateTime.UtcNow
            };

            //Act
            await _gameService.UpdateGameAsync(gameDto);
            var game = await _gameService.GetGameAsync(3);

            //Assert
            Assert.AreEqual("8-0", game.Result);
        }
    }
    }
