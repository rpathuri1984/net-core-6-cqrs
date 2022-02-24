using MediatR;
using Moq;
using NUnit.Framework;
using RYB.Business;
using RYB.Cryptography;
using RYB.MediatR.Handlers;
using RYB.Model.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RYB.MediatR.Tests;

public class UserCommandHandlerTests
{
    UserHandler getUserHandler;
    Mock<IUserRepo> userRepoMock;
    Mock<IJwtUtils> jwtUtilsMock;
    Mock<IMediator> _mediatRMock;
    public UserCommandHandlerTests()
    {
        userRepoMock = new Mock<IUserRepo>();
        jwtUtilsMock = new Mock<IJwtUtils>();
        _mediatRMock = new Mock<IMediator>();
        getUserHandler = new UserHandler(userRepoMock.Object, jwtUtilsMock.Object, _mediatRMock.Object);

    }
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        // arrange
        IEnumerable<UserProfile> users = new List<UserProfile>();
        userRepoMock.Setup(x => x.GetUsers()).ReturnsAsync(users);

        // act
        Task<IEnumerable<UserProfile>> usersData = getUserHandler.Handle(new Requests.GetUsersQuery(), default(System.Threading.CancellationToken));
    }
}
