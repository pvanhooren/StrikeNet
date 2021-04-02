using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using StrikeNet.EntityFramework.DbContext;
using StrikeNet.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrikeNet.BusinessLogicTests.Identity
{
    //Geprobeerd te testen, maar te omslachtig

    //public class FakeUserManager : UserManager<UserIdentity>
    //{
    //    public FakeUserManager(Mock<IServiceProvider> mock)
    //        : base(new Mock<IQueryableUserStore<UserIdentity>>().Object,
    //              new Mock<IOptions<IdentityOptions>>().Object,
    //              new Mock<IPasswordHasher<UserIdentity>>().Object,
    //              new IUserValidator<UserIdentity>[0],
    //              new IPasswordValidator<UserIdentity>[0],
    //              new Mock<ILookupNormalizer>().Object,
    //              new Mock<IdentityErrorDescriber>().Object,
    //              mock.Object,
    //              new Mock<ILogger<UserManager<UserIdentity>>>().Object)
    //    { 
    //    }

    }
