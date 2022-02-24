using Microsoft.Extensions.Options;
using NUnit.Framework;
using RYB.Cryptography;
using RYB.Model.AppSettings;
using RYB.Model.ViewModel;

namespace RYB.Business.Tests;

public class JwtUtilityTests
{
    private JwtUtils jwtUtils;
    public JwtUtilityTests()
    {
        IOptions<Jwt> options = Options.Create(new Jwt { Secret = "THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACE IT WITH YOUR OWN SECRET, IT CAN BE ANY STRING", RefreshTokenTTL = 10 });
        jwtUtils = new JwtUtils(options);
    }

    [Test]
    public void GenerateJwtToken_should_return_validtoken()
    {
        // arrange
        UserToken user = new UserToken {
            FirstName = "Some Name",
            Id = 1234
        };

        // act
        var token = jwtUtils.GenerateJwtToken(user);
        var actualUserId = jwtUtils.ValidateJwtToken(token);

        // assert
        Assert.IsNotNull(token);
        Assert.AreEqual(user.Id.ToString(), actualUserId);
    }


    [Test]
    [TestCase("192.168.0.1", ExpectedResult = "192.168.0.1")]
    [TestCase("192.168.0.2", ExpectedResult = "192.168.0.2")]
    [TestCase("192.168.0.3", ExpectedResult = "192.168.0.3")]
    public string GenerateRefreshToken_should_return_valid_uniuque_token_for_given_ipaddress(string ipAddress)
    {
        // arrangeId

        // act
        var token = jwtUtils.GenerateRefreshToken(ipAddress);

        // assert
        Assert.IsNotNull(token);
        return token.CreatedByIp;
    }
}

