using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StrikeNet.BusinessLogic.Services;
using StrikeNet.EntityFramework.Repositories.Interface;
using StrikeNet.EntityFramework.Repositories;
using StrikeNet.EntityFramework.DbContext;
using StrikeNet.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StrikeNet.BusinessLogic.Dtos;
using StrikeNet.EntityFramework.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace StrikeNet.BusinessLogic.Services.Tests
{
    [TestClass()]
    public class PredictionServiceTests
    {
        private PredictionService _predictionService;
        private readonly UserManager<UserIdentity> _userManager;
        private readonly RoleManager<RoleIdentity> _roleManager;
        private readonly SignInManager<UserIdentity> _signInManager;
        private PredictionService _predictionService2;
        private Guid? User1 = Guid.NewGuid();
        private Guid? User2 = Guid.NewGuid();
        private Guid? User3 = Guid.NewGuid();
        private Guid? User4 = Guid.NewGuid();
        private UserIdentity TestUser;

        //public PredictionServiceTests()
        //{
        //    var predictionRepository = new Mock<IPredictionRepository>();
        //    predictionRepository.Setup(x => x.CheckTypeResult(It.IsAny<string>())).ReturnsAsync((string result) =>
        //   {
        //       if (Convert.ToInt32(result.Substring(0, 1)) > Convert.ToInt32(result.Substring(2, 1))) return TypeResult.WinTeam1;
        //       else if (Convert.ToInt32(result.Substring(0, 1)) < Convert.ToInt32(result.Substring(2, 1))) return TypeResult.WinTeam2;
        //       else if (Convert.ToInt32(result.Substring(0, 1)) == Convert.ToInt32(result.Substring(2, 1))) return TypeResult.Draw;
        //       else return TypeResult.NNB;
        //   });

        //    predictionRepository.Setup(x => x.CompareResultsAsync(It.IsAny<Game>(), It.IsAny<Prediction>())).ReturnsAsync((Game z, Prediction y) =>
        //   {
        //       if (y.PredictedResult == z.Result)
        //           return 3;
        //       else if (y.PredictedTypeResult == z.typeResult)
        //           return 1;
        //       else
        //           return 0;
        //   });
        //    _predictionService = new PredictionService(predictionRepository.Object);
        //}

        public async Task CreateDb()
        {
            var options = new DbContextOptionsBuilder<StrikeNetDbContext>()
                .UseInMemoryDatabase(databaseName: "StrikeNetTestDb")
                .EnableSensitiveDataLogging()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;

            var dbContext = new StrikeNetDbContext(options);

            if (await dbContext.Predictions.CountAsync() <= 0)
            {
                Prediction p1 = new Prediction()
                {
                    Id = 1,
                    GameId = 1,
                    UserId = User1,
                    PredictedResult = "3-0",
                    PredictedTypeResult = TypeResult.WinTeam1
                };
                Prediction p2 = new Prediction()
                {
                    Id = 2,
                    GameId = 3,
                    UserId = User2,
                    PredictedResult = "2-0",
                    PredictedTypeResult = TypeResult.WinTeam1
                };
                Prediction p3 = new Prediction()
                {
                    Id = 3,
                    GameId = 3,
                    UserId = User3,
                    PredictedResult = "0-1",
                    PredictedTypeResult = TypeResult.WinTeam2
                };
                Prediction p4 = new Prediction()
                {
                    Id = 4,
                    GameId = 4,
                    UserId = User4,
                    PredictedResult = "1-1",
                    PredictedTypeResult = TypeResult.Draw
                };
                dbContext.Predictions.Add(p1);
                dbContext.Predictions.Add(p2);
                dbContext.Predictions.Add(p3);
                dbContext.Predictions.Add(p4);
                await dbContext.SaveChangesAsync();
            }

            var identityRepository = new IdentityRepository(_userManager, _roleManager, _signInManager);
            var predictionRepository = new PredictionRepository(identityRepository, dbContext);
            _predictionService2 = new PredictionService(predictionRepository);
        }

        public async Task CreateDbWithMockIdentity()
        {
            var options = new DbContextOptionsBuilder<StrikeNetDbContext>()
                .UseInMemoryDatabase(databaseName: "StrikeNetTestDb")
                .EnableSensitiveDataLogging()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;

            var _dbContext = new StrikeNetDbContext(options);

            TestUser = new UserIdentity { Score = 0 };
            var identityRepository = new Mock<IIdentityRepository>();
            identityRepository.Setup(x => x.GetUserAsync(It.IsAny<Guid?>())).ReturnsAsync(TestUser);
            var predictionRepository = new PredictionRepository(identityRepository.Object, _dbContext);
            _predictionService = new PredictionService(predictionRepository);
        }

        [TestMethod()]
        public async Task CompareResultsAsyncTest()
        {
            //Arrange
            //await CreateNewDbInstance();
            await CreateDb();

            GameDto game = new GameDto();
            game.Result = "2-1";
            PredictionDto prediction = new PredictionDto();
            prediction.PredictedResult = "2-1";

            //Act
            var points = await _predictionService2.CompareResultsAsync(game, prediction);

            //Assert
            Assert.AreEqual(3, points);
        }

        [TestMethod()]
        public async Task CompareResultsAsyncTest1()
        {
            //Arrange
            //await CreateNewDbInstance();
            await CreateDb();

            GameDto game = new GameDto();
            game.Result = "2-2";
            PredictionDto prediction = new PredictionDto();
            prediction.PredictedResult = "1-1";

            //Act
            var points = await _predictionService2.CompareResultsAsync(game, prediction);

            //Assert
            Assert.AreEqual(1, points);
        }

        [TestMethod()]
        public async Task CompareResultsAsyncTest2()
        {
            //Arrange
            //await CreateNewDbInstance();
            await CreateDb();

            GameDto game = new GameDto();
            game.Result = "0-2";
            game.typeResult = await _predictionService2.CheckTypeResult(game.Result);
            PredictionDto prediction = new PredictionDto();
            prediction.PredictedResult = "3-0";
            prediction.PredictedTypeResult = await _predictionService2.CheckTypeResult(prediction.PredictedResult);

            //Act
            var points = await _predictionService2.CompareResultsAsync(game, prediction);

            //Assert
            Assert.AreEqual(0, points);
        }

        //[TestMethod()]
        //public async Task GetPredictionAsyncTest()
        //{
        //    //Arrange
        //    await CreateDb();

        //    //Act
        //    var prediction = await _predictionService2.GetPredictionAsync(3, User3);

        //    Assert
        //    Assert.AreEqual("0-1", prediction.PredictedResult);
        //}

        [TestMethod()]
        public async Task SavePredictionAsyncTest()
        {
            //Arrange
            await CreateDb();

            //Act
            await _predictionService2.SavePredictionAsync(5, User4, "1-2");
            var prediction = await _predictionService2.GetPredictionAsync(5, User4);

            //Assert
            Assert.AreEqual("1-2", prediction.PredictedResult);
        }

        [TestMethod()]
        public async Task UpdatePredictionAsyncTest()
        {
            //Arrange
            await CreateDb();

            //Act
            await _predictionService2.UpdatePredictionAsync(4, User4, "2-1");
            var prediction = await _predictionService2.GetPredictionAsync(4, User4);

            //Assert
            Assert.AreEqual("2-1", prediction.PredictedResult);
        }

        [TestMethod()]
        public async Task AddPointsAsyncTest()
        {
            //Arrange
            await CreateDbWithMockIdentity();

            //Act
            await _predictionService.AddPointsAsync(User1, 1, 3);
            var prediction = await _predictionService.GetPredictionAsync(1, User1);

            //Assert
            Assert.AreEqual(3, TestUser.Score);
        }
    }
}