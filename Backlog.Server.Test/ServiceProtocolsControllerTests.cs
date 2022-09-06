namespace Backlog.Server.Test
{
    using Backlog.Server.Features.ServiceProtocols;
    using Backlog.Server.Features.ServiceProtocols.Models;
    using Backlog.Server.Utilities;
    using FakeItEasy;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Security.Claims;

    public class ServiceProtocolsControllerTests
    {
        private static IServiceProtocolService fakeService = A.Fake<IServiceProtocolService>();
        private static ServiceProtocolsController controller;
        private static Random rnd = new Random();
        
        [SetUp]
        public void Setup()
        {
            controller = new ServiceProtocolsController(fakeService);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "TestUser"),
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim("test-custom-claim", "test claim value"),
            }, "mock"));

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            var guid = Guid.NewGuid();
            A.CallTo(() => fakeService.GetServiceProtocolById(A<int>._)).Returns(new ServiceProtocol { UserId = guid.ToString() });
        }

        [Test]
        public void Create_StatusCode_201_Created()
        {
            //Arrange
            var fakeObj = A.Fake<ServiceProtocol>();
            int expectedStatus = (int)HttpStatusCode.Created;
            A.CallTo(() => fakeService.CreateServiceProtocol(A<ServiceProtocol>.Ignored)).Returns(A.Fake<ServiceProtocol>());

            //Act
            var response = controller.Create(fakeObj);
            var actualResult = response.Result as CreatedResult;

            //Assert
            Assert.AreEqual(expectedStatus, actualResult.StatusCode);
        }

        [Test]
        public void GetList_StatusCode_200_Ok()
        {
            //Arrange
            int expectedStatus = (int)HttpStatusCode.OK;

            //Act
            var response = controller.GetList();
            var actualResult = response.Result.Result as OkObjectResult;

            //Assert
            Assert.AreEqual(expectedStatus, actualResult.StatusCode);
        }

        [Test]
        public void GetList_Return_List_With_Two_ServiceProtocols()
        {
            //Arrange
            A.CallTo(() => fakeService.GetServiceProtocolsList(A<string>.Ignored)).
                Returns(
                new List<ServiceProtocol>
                {
                    new ServiceProtocol(),
                    new ServiceProtocol()
                });

            //Act
            var response = controller.GetList();
            var actualResult = response.Result.Result as OkObjectResult;
            var actualList = actualResult.Value as List<ServiceProtocolListResponseModel>;

            //Assert
            Assert.That(actualList.Count, Is.EqualTo(2));
        }

        [Test]
        public void Edit_StatusCode_400_BadRequest()
        {
            //Arrange
            var fakeObj = A.Fake<ServiceProtocol>();
            var id = rnd.Next(1,1000);
            A.CallTo(() => fakeService.CreateServiceProtocol(A<ServiceProtocol>.Ignored)).Returns(new ServiceProtocol { Id = id });
            var createResponse = controller.Create(fakeObj);
            var actualCreateResult = createResponse.Result as CreatedResult;
            var actualId = int.Parse(actualCreateResult.Value.ToString());
            int expectedStatus = (int)HttpStatusCode.BadRequest;

            //Act
            var response = controller.Edit(actualId, A.Fake<ServiceProtocol>());
            var actualResult = response.Result as BadRequestObjectResult;
            var actualResultMsg = actualResult.Value;

            //Assert
            Assert.That(actualResult.StatusCode, Is.EqualTo(expectedStatus));
            Assert.That(actualResultMsg, Is.EqualTo(Messages.NotValidServiceProtocolId));
        }

        [Test]
        public void Edit_StatusCode_401_Unauthorized()
        {
            //Arrange
            int expectedStatus = (int)HttpStatusCode.Unauthorized;

            //Act
            var response = controller.Edit(0, A.Fake<ServiceProtocol>());
            var actualResult = response.Result as UnauthorizedObjectResult;
            var actualResultMsg = actualResult.Value;

            //Assert
            Assert.That(actualResult.StatusCode, Is.EqualTo(expectedStatus));
            Assert.That(actualResultMsg, Is.EqualTo(Messages.Unauthorized));
        }

        [Test]
        public void Edit_StatusCode_204_NoContent_SuccessfullyEdited()
        {
            //Arrange
            int expectedStatus = (int)HttpStatusCode.NoContent;
            A.CallTo(() => fakeService.GetServiceProtocolById(A<int>._)).Returns(new ServiceProtocol { UserId = "1" });

            //Act
            var response = controller.Edit(0, A.Fake<ServiceProtocol>());
            var actualResult = response.Result as NoContentResult;

            //Assert
            Assert.That(actualResult.StatusCode, Is.EqualTo(expectedStatus));
        }

        [Test]
        public void GetById_StatusCode_404_NotFound() 
        {
            //Arrange
            int expectedStatus = (int)HttpStatusCode.NotFound;
            int serviceProtocolId = rnd.Next(1, 1000);
            A.CallTo(() => fakeService.GetServiceProtocolById(A<int>.Ignored)).Returns(new ServiceProtocol().Id == 0 ? null : new ServiceProtocol());

            //Act
            var response = controller.GetById(serviceProtocolId);
            var actualResult = response.Result.Result as NotFoundObjectResult;

            //Assert
            Assert.That(actualResult.StatusCode, Is.EqualTo(expectedStatus));
            Assert.That(actualResult.Value, Is.EqualTo(string.Format(Messages.NotFoundWithId, serviceProtocolId)));
        }

        [Test]
        public void GetById_StatusCode_401_Unauthorized() 
        {
            //Arrange
            int expectedStatus = (int)HttpStatusCode.Unauthorized;

            //Act
            var response = controller.GetById(0);
            var actualResult = response.Result.Result as UnauthorizedObjectResult;
            var actualResultMsg = actualResult.Value;

            //Assert
            Assert.That(actualResult.StatusCode, Is.EqualTo(expectedStatus));
        }

        [Test]
        public void GetById_StatusCode_200_Ok_Should_Return_ServiceProtocol()
        {
            //Arrange
            int expectedStatus = (int)HttpStatusCode.OK;
            A.CallTo(() => fakeService.GetServiceProtocolById(A<int>._)).Returns(new ServiceProtocol { UserId = "1" });

            //Act
            var response = controller.GetById(0);
            var actualResult = response.Result.Result as OkObjectResult;
            var actualResultObj = actualResult.Value;

            //Assert
            Assert.IsNotNull(actualResultObj);
            Assert.That(actualResult.StatusCode, Is.EqualTo(expectedStatus));
        }

        [Test]
        public void Search_StatusCode_200_Ok() 
        {
            //Arrange
            int expectedStatus = (int)HttpStatusCode.OK;

            //Act
            var response = controller.Search(string.Empty);
            var actualResult = response.Result.Result as OkObjectResult;

            //Assert
            Assert.AreEqual(expectedStatus, actualResult.StatusCode);
        }

        [Test]
        public void Search_Return_List_With_Tree_ServiceProtocols()
        {
            //Arrange
            A.CallTo(() => fakeService.Search(A<string>.Ignored, "1")).
                Returns(
                new List<ServiceProtocol>
                {
                    new ServiceProtocol(),
                    new ServiceProtocol(),
                    new ServiceProtocol()
                });

            //Act
            var response = controller.Search(string.Empty);
            var actualResult = response.Result.Result as OkObjectResult;
            var actualList = actualResult.Value as List<ServiceProtocolListResponseModel>;
            
            //Assert
            Assert.That(actualList.Count, Is.EqualTo(3));
        }

        [Test]
        public void SetStatus_StatusCode_404_NotFound()
        {
            //Arrange
            int expectedStatus = (int)HttpStatusCode.NotFound;
            int serviceProtocolId = rnd.Next(1, 1000);
            A.CallTo(() => fakeService.GetServiceProtocolById(A<int>.Ignored)).Returns(new ServiceProtocol().Id == 0 ? null : new ServiceProtocol());

            //Act
            var response = controller.SetStatus(1, 1);
            var actualResult = response.Result as NotFoundObjectResult;

            //Assert
            Assert.That(actualResult.StatusCode, Is.EqualTo(expectedStatus));
        }

        [Test]
        public void SetStatus_StatusCode_401_Unauthorized() 
        {
            //Arrange
            int expectedStatus = (int)HttpStatusCode.Unauthorized;

            //Act
            var response = controller.SetStatus(1, 1);
            var actualResult = response.Result as UnauthorizedObjectResult;
            var actualResultMsg = actualResult.Value;

            //Assert
            Assert.That(actualResult.StatusCode, Is.EqualTo(expectedStatus));
            Assert.That(actualResultMsg, Is.EqualTo(Messages.Unauthorized));
        }

        [Test]
        public void SetStatus_StatusCode_202_Accepted()
        {
            //Arrange
            int expectedStatus = (int)HttpStatusCode.Accepted;
            A.CallTo(() => fakeService.GetServiceProtocolById(A<int>._)).Returns(new ServiceProtocol { UserId = "1" });
            
            //Act
            var response = controller.SetStatus(1, 1);
            var actualResult = response.Result as AcceptedResult;
            var actualResultMsg = actualResult.Location;

            //Assert
            Assert.That(actualResult.StatusCode, Is.EqualTo(expectedStatus));
            Assert.That(actualResultMsg, Is.EqualTo(Messages.StatusWasUpdated));
        }

        [Test]
        public void DeleteServiceProtocol_StatusCode_400_BadRequest() 
        {
            //Arrange
            var fakeObj = A.Fake<ServiceProtocol>();
            A.CallTo(() => fakeService.CreateServiceProtocol(A<ServiceProtocol>.Ignored)).Returns(new ServiceProtocol());
            var createResponse = controller.Create(fakeObj);
            var actualCreateResult = createResponse.Result as CreatedResult;
            var actualId = int.Parse(actualCreateResult.Value.ToString());
            int expectedStatus = (int)HttpStatusCode.BadRequest;

            //Act
            var response = controller.DeleteServiceProtocol(actualId);
            var actualResult = response.Result as BadRequestObjectResult;
            var actualResultMsg = actualResult.Value;

            //Assert
            Assert.That(actualResult.StatusCode, Is.EqualTo(expectedStatus));
            Assert.That(actualResultMsg, Is.EqualTo(Messages.NotValidServiceProtocolId));
        }

        [Test]
        public void DeleteServiceProtocol_StatusCode_401_Unauthorized()
        {
            //Arrange
            int expectedStatus = (int)HttpStatusCode.Unauthorized;

            //Act
            var response = controller.DeleteServiceProtocol(1);
            var actualResult = response.Result as UnauthorizedObjectResult;
            var actualResultMsg = actualResult.Value;

            //Assert
            Assert.That(actualResult.StatusCode, Is.EqualTo(expectedStatus));
            Assert.That(actualResultMsg, Is.EqualTo(Messages.Unauthorized));
        }

        [Test]
        public void DeleteServiceProtocol_StatusCode_404_NotFound()
        {
            //Arrange
            Random rnd = new Random();
            int expectedStatus = (int)HttpStatusCode.NotFound;
            int serviceProtocolId = rnd.Next(1, 1000);
            A.CallTo(() => fakeService.GetServiceProtocolById(A<int>.Ignored)).Returns(new ServiceProtocol().Id == 0 ? null : new ServiceProtocol());
            
            //Act
            var response = controller.DeleteServiceProtocol(serviceProtocolId);
            var actualResult = response.Result as NotFoundObjectResult;

            //Assert
            Assert.That(actualResult.StatusCode, Is.EqualTo(expectedStatus));
            Assert.That(actualResult.Value, Is.EqualTo(string.Format(Messages.NotFoundWithId, serviceProtocolId)));
        }

        [Test]
        public void DeleteServiceProtocol_StatusCode_204_NoContent_SuccessfullyDeleted()
        {
            //Arrange
            var fakeObj = A.Fake<ServiceProtocol>();
            var id = 123;
            A.CallTo(() => fakeService.CreateServiceProtocol(A<ServiceProtocol>.Ignored)).Returns(new ServiceProtocol { Id = id });
            A.CallTo(() => fakeService.GetServiceProtocolById(A<int>._)).Returns(new ServiceProtocol { UserId = "1" });
            var createResponse = controller.Create(fakeObj);
            var actualCreateResult = createResponse.Result as CreatedResult;
            var actualId = int.Parse(actualCreateResult.Value.ToString());
            int expectedStatus = (int)HttpStatusCode.NoContent;

            //Act
            var response = controller.DeleteServiceProtocol(actualId);
            var actualResult = response.Result as NoContentResult;

            //Assert
            Assert.That(actualResult.StatusCode, Is.EqualTo(expectedStatus));
        }
    }
}