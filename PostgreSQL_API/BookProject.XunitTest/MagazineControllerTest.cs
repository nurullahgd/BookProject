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
using AutoMapper;
using BookProject.Application.Mapper;

namespace BookProject.XunitTest
{
   public class MagazineControllerTest
    {
        private Mock<IMagazineRepository> _mock;
        private readonly MagazineService _magazineService;
        private readonly MagazineController _magazineController;
        IMapper mapper = BookProjectMapper.Mapper;

        public MagazineControllerTest()
        {
            _mock = new Mock<IMagazineRepository>();
            _magazineService = new MagazineService(_mock.Object);
            _magazineController = new MagazineController(_magazineService);
            
        }
        [Fact]
        public void Get_Returns_Correct_Id()
        {
            // Arrange
            _mock.Setup(y => y.GetByIdAsync(1)).ReturnsAsync(FakeData());
            // Act
            var actionResult = _magazineController.Get(1);
            var okObjectResult = actionResult.Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(okObjectResult);
            var magazine = Assert.IsType<MagazineResponse>(okObjectResult.Value);
            Assert.Equal("test", magazine.Name);

        }
        [Fact]
        public void Get_Returns_Wrong_Id()
        {
            // Arrange
            _mock.Setup(y => y.GetByIdAsync(1)).ReturnsAsync(FakeData());
            // Act
            var actionResult = _magazineController.Get(2);
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
            var actionResult = _magazineController.Get(0);
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
            var actionResult = _magazineController.GetAll();
            var okObjectResult = actionResult.Result as OkObjectResult;

            // Assert
            Assert.Equal(okObjectResult.StatusCode, (int)okObjectResult.StatusCode);

        }
        [Fact]
        public void Create_Return_Correctly()
        {
            // Arrange
            var magazineToCreate = FakeData();
            Magazine addedMagazine = mapper.Map<Magazine>(magazineToCreate);
            _mock.Setup(y => y.AddAsync(addedMagazine).Result).Returns(addedMagazine);
            // Act
            var actionResult = _magazineController.Create(mapper.Map<MagazineModel>(addedMagazine));
            var okObjectResult = actionResult.Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(okObjectResult);

        }
        [Fact]
        public void Update_Return_Correctly()
        {

            // Arrange
            var magazineToUpdate = FakeData();
            magazineToUpdate.Name = "This is updated Name";
            _mock.Setup(y => y.GetByIdAsync(1)).ReturnsAsync(FakeData());
            _mock.Setup(y => y.UpdateAsync(magazineToUpdate)).ReturnsAsync(magazineToUpdate);
            MagazineModel updatedMagazineModel = new MagazineModel { Id = 1, Name = "This is updated Name" };

            // Act
            var actionResult = _magazineController.Update(1, updatedMagazineModel);
            var okObjectResult = actionResult.Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(okObjectResult);

        }
        [Fact]
        public void Delete_Return_Correctly()
        {
            // Arrange
            _mock.Setup(y => y.GetByIdAsync(1)).ReturnsAsync(FakeData());
            // Act
            var actionResult = _magazineController.Delete(1).Result;

            // Assert
            Assert.IsType<OkObjectResult>(actionResult);

        }
        [Fact]
        public void Delete_returns_NotFound_InvalidId()
        {
            // Arrange
            _mock.Setup(y => y.GetByIdAsync(1)).ReturnsAsync(FakeData());
            // Act
            var actionResult = _magazineController.Delete(4).Result;
            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }
        public Magazine FakeData()
        {
            return new Magazine
            {
                Id=1,
                Name="test"

            };
        }
    }
}
