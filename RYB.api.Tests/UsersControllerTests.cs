using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using RYB.api.Controllers;
using RYB.Model.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RYB.api.Tests
{
    public class UsersControllerTests
    {
        private UsersController usersController;
        private Mock<IMediator> mediatRMock;
        private Mock<ILogger<UsersController>> loggerMock;

        public UsersControllerTests()
        {
            mediatRMock = new Mock<IMediator>();
            loggerMock = new Mock<ILogger<UsersController>>();
            usersController = new UsersController(mediatRMock.Object, loggerMock.Object);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetUsers_should_return_Ok_result()
        {
            // arrange
            IEnumerable<UserProfile> expectedData = new List<UserProfile> {
            new UserProfile {Email ="test@email.com"}
            };
            mediatRMock.Setup(x => x.Send(It.IsAny<MediatR.Requests.GetUsersQuery>(), default(System.Threading.CancellationToken))).ReturnsAsync(expectedData);

            // act
            Task<IActionResult> actionResult = usersController.GetUsers();
            actionResult.Wait();



            OkObjectResult? okObjectResult = actionResult.Result as OkObjectResult;
            Assert.NotNull(okObjectResult);

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            IEnumerable<UserProfile>? actualData = okObjectResult.Value as IEnumerable<UserProfile>;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            Assert.NotNull(actualData);

            // assert
#pragma warning disable CS8604 // Possible null reference argument.
            Assert.AreEqual(expectedData.First().Email, actualData.First().Email);
#pragma warning restore CS8604 // Possible null reference argument.

        }
    }
}