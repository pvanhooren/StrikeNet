using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StrikeNet.BusinessLogic.Services;
using StrikeNet.BusinessLogicTests.Identity;
using StrikeNet.EntityFramework.Entities;
using StrikeNet.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MockQueryable;
using MockQueryable.Moq;
using StrikeNet.BusinessLogic.Dtos;
using StrikeNet.EntityFramework.DbContext;
using Microsoft.EntityFrameworkCore;

namespace StrikeNet.BusinessLogic.Services.Tests
{
    //Geprobeerd te testen, maar te omslachtig

    //[TestClass()]
    //public class IdentityServiceTests
    //{
    //    private IdentityService _identityService;
    //    Guid UserId1 = Guid.NewGuid();
    //    Guid UserId2 = Guid.NewGuid();
    //    Guid UserId3 = Guid.NewGuid();
    //    Guid UserId4 = Guid.NewGuid();
    //    Guid RoleId1 = Guid.NewGuid();
    //    Guid RoleId2 = Guid.NewGuid();

    //    public IdentityServiceTests()
    //    {
            
    //    }

    //    public void CreateUserManager()
    //    {
            //var userStore = new Mock<IUserStore<UserIdentity>>();
            //var options = new Mock<IOptions<IdentityOptions>>();
            //var hasher = new Mock<IPasswordHasher<UserIdentity>>();
            //var uvalidator = new Mock <IEnumerable<IUserValidator<UserIdentity>>>();
            //var pvalidator = new Mock <IEnumerable<IPasswordValidator<UserIdentity>>>();
            //var normalizer = new Mock<ILookupNormalizer>();
            //var error = new Mock<IdentityErrorDescriber>();
            //var service = new Mock<IServiceProvider>();
            //var logger = new Mock<ILogger<UserManager<UserIdentity>>>();
            //var userManager = new UserManager<UserIdentity>(userStore.Object, options.Object, hasher.Object, uvalidator.Object, pvalidator.Object, normalizer.Object, error.Object, service.Object, logger.Object);
            //var _userManager = new Mock<UserManager<UserIdentity>>(userStore.Object, null, null, null, null, null, null, null);
            //UserIdentity user1 = new UserIdentity() { Id = UserId1, UserName = "test@gmail.com", Score = 5 };
            //UserIdentity user2 = new UserIdentity() { Id = UserId2, UserName = "pragim@gmail.com", Score = 1 };
            //UserIdentity user3 = new UserIdentity() { Id = UserId3, UserName = "ajax@gmail.com", Score = 0 };
            //UserIdentity user4 = new UserIdentity() { Id = UserId4, UserName = "pim@gmail.com", Score = 4 };
            //List<UserIdentity> users = new List<UserIdentity>() { user1, user2, user3, user4 };
            //_userManager.Setup(x => x.Users).Returns(users.AsQueryable()) ;
            //_userManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user1);

            //Mocking UserManager
            //var _userManager = new UserManager<UserIdentity>(new Mock<IQueryableUserStore<UserIdentity>>().Object, new Mock<IOptions<IdentityOptions>>().Object,
            //      new Mock<IPasswordHasher<UserIdentity>>().Object,
            //      new IUserValidator<UserIdentity>[0],
            //      new IPasswordValidator<UserIdentity>[0],
            //      new Mock<ILookupNormalizer>().Object,
            //      new Mock<IdentityErrorDescriber>().Object,
            //      new Mock<IServiceProvider>().Object,
            //      new Mock<ILogger<UserManager<UserIdentity>>>().Object);
            //var _userManager = new Mock<FakeUserManager>();
            //var mock = new Mock<IServiceProvider>();
            //mock.Setup(x => x.GetService(typeof(StrikeNetDbContext))).Returns(new StrikeNetDbContext(new DbContextOptions<StrikeNetDbContext>()));
            //var _userManager = new FakeUserManager(mock);
            //UserIdentity user1 = new UserIdentity() { Id = UserId1, UserName = "test@gmail.com", Score = 5 };
            //UserIdentity user2 = new UserIdentity() { Id = UserId2, UserName = "pragim@gmail.com", Score = 1 };
            //UserIdentity user3 = new UserIdentity() { Id = UserId3, UserName = "ajax@gmail.com", Score = 0 };
            //UserIdentity user4 = new UserIdentity() { Id = UserId4, UserName = "pim@gmail.com", Score = 4 };
            //List<UserIdentity> users = new List<UserIdentity>() { user1, user2, user3, user4 };
            //_userManager.CreateAsync(user1);
            //_userManager.CreateAsync(user2);
            //_userManager.CreateAsync(user3);
            //_userManager.CreateAsync(user4);
            //_userManager.Setup(x => x.Users).Returns(users.AsQueryable());
            //_userManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).Returns(user1);
            //_userManager.Setup(x => x.DeleteAsync(It.IsAny<UserIdentity>())).Returns(IdentityResult.Success);

            //Mocking RoleManager
            //var _roleManager = new Mock<RoleManager<RoleIdentity>>();
            //RoleIdentity role1 = new RoleIdentity() { Id = RoleId1, Name = "User" };
            //RoleIdentity role2 = new RoleIdentity() { Id = RoleId2, Name = "Admin" };
            //List<RoleIdentity> roles = new List<RoleIdentity>() { role1, role2 };
            //_roleManager.Setup(x => x.Roles).Returns(roles.AsQueryable());
            //var _signInManager = new Mock<SignInManager<UserIdentity>>();
        //    var identityRepository = new IdentityRepository(_userManager, null, null);

        //    _identityService = new IdentityService(identityRepository);
        //}

        //[TestMethod()]
        //public void GetUserAsyncTest()
        //{
        //    //Arrange
        //    CreateUserManager();

        //    //Act
        //    var user = _identityService.GetUserAsync(UserId4).Result;

        //    //Assert
        //    Assert.AreEqual("pim@gmail.com", user.UserName);
        //}

        //[TestMethod()]
        //public void DeleteUserAsyncTest()
        //{
        //    //Arrange
        //    CreateUserManager();

        //    var user = new UserDto
        //    {
        //        Id = UserId1,
        //        UserName = "test@gmail.com",
        //        Score = 5
        //    };

        //    //Act
        //    var result = _identityService.DeleteUserAsync(user).Result;

        //    //Assert
        //    Assert.AreEqual(IdentityResult.Success, result);
        //}
    //}
}