using BookProject.Application.Models;
using BookProject.Application.Services;
using BookProject.Controllers;
using BookProject.Data.Entities;
using BookProject.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Net;
using Xunit;
namespace BookProject.XunitTest
{
    public class UserControllerTest
    {
        private Mock<IUserRepository> _mock;
        private readonly UserService _userService;
        private readonly UserController _userController;
        public UserControllerTest()
        {
            _mock = new Mock<IUserRepository>();
            _userService = new UserService(_mock.Object);
            _userController = new UserController(_userService);
        }

        [Fact]
        public void Get_Returns_Correct_Id()
        {
            // Arrange
            _mock.Setup(y => y.GetByIdAsync(1)).ReturnsAsync(FakeData());
            // Act
            var actionResult = _userController.Get(1);
            var okObjectResult = actionResult.Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(okObjectResult);
            var user = Assert.IsType<User>(okObjectResult.Value);
            Assert.Equal(1, user.Id);

        }
        [Fact]
        public void Get_Returns_Wrong_Id()
        {
            // Arrange
            _mock.Setup(y => y.GetByIdAsync(1)).ReturnsAsync(FakeData());
            // Act
            var actionResult = _userController.Get(2);
            var okObjectResult = actionResult.Result as NotFoundResult;
            // Assert
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.NotFound);

        }
        [Fact]
        public void Get_Returns_Negative_Id()
        {
            // Arrange
            _mock.Setup(y => y.GetByIdAsync(1)).ReturnsAsync(FakeData());
            // Act
            var actionResult = _userController.Get(0);
            var badObjectResult = actionResult.Result as BadRequestObjectResult;

            // Assert
            Assert.Equal(badObjectResult.StatusCode, (int)HttpStatusCode.BadRequest);

        }
        [Fact]
        public void GetAll_Returns_Correctly()
        {
            // Arrange
            _mock.Setup(y => y.GetAllAsync());
            // Act
            var actionResult = _userController.GetAll();
            var okObjectResult = actionResult.Result as OkObjectResult;

            // Assert
            Assert.Equal(okObjectResult.StatusCode, (int)okObjectResult.StatusCode);

        }
        [Fact]
        public void Create_Return_Correctly()
        {
            // Arrange
            
            User added = new User() { Id = 2, FirstName = "addtest", LastName = "addtest",Email="testmail"};
            _mock.Setup(y => y.AddAsync(added).Result).Returns(added);   
            // Act
            var actionResult = _userController.Create(added);
            var okObjectResult = actionResult.Result as OkObjectResult;

            // Assert
            
            Assert.IsType<OkObjectResult>(okObjectResult);

        }
        [Fact]
        public void Update_Return_Correctly()
        {
            
            // Arrange
            var userToUpdate = FakeData();
            userToUpdate.FirstName = "New Name Test";
            _mock.Setup(y => y.GetByIdAsync(1)).ReturnsAsync(FakeData());
            _mock.Setup(y => y.UpdateAsync(userToUpdate)).ReturnsAsync(userToUpdate);

            // Act
            var actionResult =  _userController.Update(1, userToUpdate);
            var okObjectResult = actionResult.Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(okObjectResult);
            //var user = Assert.IsType<User>(okObjectResult.Value);
            //Assert.Equal(userToUpdate.Id, user.Id);
            //Assert.Equal(userToUpdate.FirstName, user.FirstName);

        }
        [Fact]
        public void Delete_Return_Correctly()
        {
            // Arrange
            _mock.Setup(y => y.GetByIdAsync(1)).ReturnsAsync(FakeData());
            // Act
            var actionResult =  _userController.Delete(1).Result;

            // Assert
            Assert.IsType<OkObjectResult>(actionResult);

        }
        [Fact]
        public void Delete_returns_NotFound_InvalidId()
        {
            // Arrange
            _mock.Setup(y => y.GetByIdAsync(1)).ReturnsAsync(FakeData());
            // Act
            var actionResult = _userController.Delete(4).Result;
            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        public User FakeData()
        {
            return new User
            {
                Id = 1,
                FirstName = "test",
                LastName = "testlast",
                Email = "tst"
            };
        }

    }
}
