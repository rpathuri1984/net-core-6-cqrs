using Moq;
using NUnit.Framework;
using RYB.Model.Dto;
using RYB.Model.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RYB.Business.Tests;

public class UserRepoTests
{
    UserRepo _userRepo;
    Mock<DataAccess.IDbAccess> _dbAccess;

    public UserRepoTests()
    {
        _dbAccess = new Mock<DataAccess.IDbAccess>();
        _userRepo = new UserRepo(_dbAccess.Object);
    }

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void GetUsers_should_return_users()
    {
        // arrange
        IEnumerable<UserProfileDto> expectedData = new List<UserProfileDto>
            {
                new UserProfileDto { Email = "test@email.com" }
            };

        _dbAccess.Setup(x => x.LoadData<UserProfileDto, dynamic>(It.IsAny<string>(), It.IsAny<object>(), System.Data.CommandType.Text, "Default")).ReturnsAsync(expectedData);

        // act
        Task<IEnumerable<UserProfile>> data = _userRepo.GetUsers();
        data.Wait();

        // assert
        Assert.AreEqual(expectedData.Count(), data.Result.Count());
        Assert.AreEqual(expectedData.First().Email, data.Result.First().Email);
    }
}