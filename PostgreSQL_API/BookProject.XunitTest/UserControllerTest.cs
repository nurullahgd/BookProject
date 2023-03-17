using BookProject.Application.Models;
using BookProject.Application.Services;
using BookProject.Controllers;
using BookProject.Data.Entities;
using BookProject.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using Xunit;
using AutoMapper;
using BookProject.Application.Mapper;
using System;

namespace BookProject.XunitTest
{
    public class UserControllerTest
    {
        private Mock<IUserRepository> _mock;
        private readonly UserService _userService;
        private readonly UserController _userController;
        IMapper mapper = BookProjectMapper.Mapper;

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
            _mock.Setup(y => y.GetByIdAsync(FakeData().Id)).ReturnsAsync(FakeData());
            // Act
            var actionResult = _userController.Get(FakeData().Id);
            var okObjectResult = actionResult.Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(okObjectResult);
            var user = Assert.IsType<UserResponse>(okObjectResult.Value);
            Assert.Equal("test", user.FirstName);

        }
        [Fact]
        public void Get_Returns_Wrong_Id()
        {
            // Arrange
            _mock.Setup(y => y.GetByIdAsync(FakeData().Id)).ReturnsAsync(FakeData());
            // Act
            var actionResult = _userController.Get(Guid.NewGuid());
            var okObjectResult = actionResult.Result as BadRequestObjectResult;
            // Assert
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.BadRequest);

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
            var userToCreate = FakeData();
            _mock.Setup(y => y.AddAsync(userToCreate).Result).Returns(userToCreate);
            // Act
            var actionResult = _userController.Create(mapper.Map<UserResponse>(userToCreate));
            var okObjectResult = actionResult.Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(okObjectResult);
        }
        [Fact]
        public void Update_Return_Correctly()
        {
            //Arrange
            var updateToUpdate = FakeData();
            updateToUpdate.FirstName = "This is updated FirstName";
            _mock.Setup(y => y.GetByIdAsync(FakeData().Id)).ReturnsAsync(FakeData());
            _mock.Setup(y => y.UpdateAsync(updateToUpdate)).ReturnsAsync(updateToUpdate);
            UserModel updatedUserModel = new UserModel { Id = FakeData().Id, FirstName = "This is updated FirstName"};

            // Act
            var actionResult = _userController.Update(FakeData().Id, updatedUserModel);
            var okObjectResult = actionResult.Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(okObjectResult);
        }
        [Fact]
        public void Delete_Return_Correctly()
        {
            // Arrange
            _mock.Setup(y => y.GetByIdAsync(FakeData().Id)).ReturnsAsync(FakeData());
            // Act
            var actionResult = _userController.Delete(FakeData().Id).Result;

            // Assert
            Assert.IsType<OkObjectResult>(actionResult);

        }
        [Fact]
        public void Delete_returns_NotFound_InvalidId()
        {
            // Arrange
            _mock.Setup(y => y.GetByIdAsync(FakeData().Id)).ReturnsAsync(FakeData());
            // Act
            var actionResult = _userController.Delete(Guid.NewGuid()).Result;
            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        public User FakeData()
        {
            return new User
            {
                Id = Guid.Parse("cdc5a40c-5462-47a5-a000-28dcf7878792"),
                FirstName = "test",
                LastName = "testlast",
                Email = "tstd@gmail.com"
            };
        }

    }
}
