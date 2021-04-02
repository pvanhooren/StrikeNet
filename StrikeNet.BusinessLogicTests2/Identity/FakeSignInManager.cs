using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using StrikeNet.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrikeNet.BusinessLogicTests.Identity
{
    //Geprobeerd te testen, maar te omslachtig

    //public class FakeSignInManager : SignInManager<UserIdentity>
    //{
    //    public FakeSignInManager()
    //        : base(new Mock<FakeUserManager>().Object,
    //              new HttpContextAccessor(),
    //              new Mock<IUserClaimsPrincipalFactory<UserIdentity>>().Object,
    //              new Mock<IOptions<IdentityOptions>>().Object,
    //              new Mock<ILogger<SignInManager<UserIdentity>>>().Object,
    //              new Mock<IAuthenticationSchemeProvider>().Object)
    //    { }
    //}
}
