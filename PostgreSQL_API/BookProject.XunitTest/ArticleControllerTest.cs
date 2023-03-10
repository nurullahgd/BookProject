using AutoMapper;
using BookProject.Application.Mapper;
using BookProject.Application.Models;
using BookProject.Application.Services;
using BookProject.Controllers;
using BookProject.Data.Entities;
using BookProject.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace BookProject.XunitTest
{
    public class ArticleControllerTest
    {
        private Mock<IArticleRepository> _mock;
        private Mock<IMagazineRepository> _mockMagazine;
        private Mock<IUserRepository> _mockUser;
        private readonly ArticleService _articleService;
        private readonly UserService _userService;
        private readonly MagazineService _magazineService;
        private readonly ArticleController _articleController;
        IMapper mapper = BookProjectMapper.Mapper;
        public ArticleControllerTest()
        {
            _mock = new Mock<IArticleRepository>();
            _mockMagazine = new Mock<IMagazineRepository>();
            _mockUser = new Mock<IUserRepository>();
            _articleService = new ArticleService(_mock.Object, mapper);
            _magazineService = new MagazineService(_mockMagazine.Object);
            _userService = new UserService(_mockUser.Object);
            _articleController = new ArticleController(_userService,_articleService,_magazineService);
        }
        [Fact]
        public void Get_Returns_Correct_Id()
        {
            // Arrange
            var articleToGet = FakeData();
            _mock.Setup(y => y.GetByIdAsync(1)).ReturnsAsync(FakeData());
            // Act
            var actionResult = _articleController.Get(1);
            var okObjectResult = actionResult.Result as OkObjectResult;

            // Assert
            var articleMap=mapper.Map<ArticleModel>(articleToGet);
            Assert.IsType<OkObjectResult>(okObjectResult);
            

        }
        [Fact]
        public void Get_Returns_Wrong_Id()
        {
            // Arrange
            _mock.Setup(y => y.GetByIdAsync(1)).ReturnsAsync(FakeData());
            // Act
            var actionResult = _articleController.Get(2);
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
            var actionResult = _articleController.Get(0);
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
            var actionResult = _articleController.GetAll();
            var okObjectResult = actionResult.Result as OkObjectResult;

            // Assert
            Assert.Equal(okObjectResult.StatusCode, (int)okObjectResult.StatusCode);

        }
        [Fact]
        public async Task Create_Return_Correctly()
        {
            // Arrange
            var added = FakeData();
            var articleModel = mapper.Map<ArticleModel>(added);

            _mockUser.Setup(x => x.GetByIdAsync(articleModel.AuthorId))
                            .ReturnsAsync(new User());

            _mockMagazine.Setup(x => x.GetByIdAsync(articleModel.MagazineId))
                                .ReturnsAsync(new Magazine());

            _mock.Setup(x => x.GetByIdAsync(articleModel.id))
                               .ReturnsAsync(null as Article);

            _mock.Setup(x => x.AddAsync(added))
                               .ReturnsAsync(added);

            // Act
            var actionResult =  _articleController.Create(articleModel);
            var okObjectResult = actionResult.Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(okObjectResult);
        }

        [Fact]
        public void Update_Return_Correctly()
        {

            // Arrange
            var articleToUpdate = FakeData();
            articleToUpdate.Content = "This is updated Content";
            _mock.Setup(y => y.GetByIdAsync(1)).ReturnsAsync(FakeData());
            _mock.Setup(y => y.UpdateAsync(articleToUpdate)).ReturnsAsync(articleToUpdate);
            ArticleModel updatedArticleModel = new ArticleModel { id = 1, Content = "This is updated Content" };

            // Act
            var actionResult = _articleController.Update(1, updatedArticleModel);
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
            var actionResult = _articleController.Delete(1).Result;

            // Assert
            Assert.IsType<OkObjectResult>(actionResult);

        }
        [Fact]
        public void Delete_returns_NotFound_InvalidId()
        {
            // Arrange
            _mock.Setup(y => y.GetByIdAsync(1)).ReturnsAsync(FakeData());
            // Act
            var actionResult = _articleController.Delete(4).Result;
            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }
        public Article FakeData()
        {
            return new Article
            {
                id=1,
                Title="test",
                Content="test",
                MagazineId=1,
                AuthorId=1
            };
        }
    }
}
