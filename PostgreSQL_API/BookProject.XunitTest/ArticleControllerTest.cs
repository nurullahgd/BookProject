using AutoMapper;
using BookProject.Application.Mapper;
using BookProject.Application.Models;
using BookProject.Application.Services;
using BookProject.Controllers;
using BookProject.Data.Entities;
using BookProject.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace BookProject.XunitTest
{
    public class ArticleControllerTest
    {
        private Mock<IArticleRepository> _mock;
        private Mock<IMagazineRepository> _mockmagazine;
        private Mock<IUserRepository> _mockuser;
        private readonly ArticleService _articleservice;
        private readonly UserService _userservice;
        private readonly MagazineService _magazineservice;
        private readonly ArticleController _articlecontroller;
        IMapper mapper = BookProjectMapper.Mapper;
        public ArticleControllerTest()
        {
            _mock = new Mock<IArticleRepository>();
            _mockmagazine = new Mock<IMagazineRepository>();
            _mockuser = new Mock<IUserRepository>();
            _articleservice = new ArticleService(_mock.Object, mapper);
            _magazineservice = new MagazineService(_mockmagazine.Object);
            _userservice = new UserService(_mockuser.Object);
            _articlecontroller = new ArticleController(_userservice, _articleservice, _magazineservice);
        }
        [Fact]
        public void Get_Returns_Correct_Id()
        {
            // arrange
            var articletoget = FakeData();
            _mock.Setup(y => y.GetByIdAsync(FakeData().Id)).ReturnsAsync(FakeData());
            // act
            var actionresult = _articlecontroller.Get(FakeData().Id);
            var okobjectresult = actionresult.Result as OkObjectResult;

            // assert
            var articlemap = mapper.Map<ArticleModel>(articletoget);
            Assert.IsType<OkObjectResult>(okobjectresult);


        }
        [Fact]
        public void Get_Returns_Wrong_Id()
        {
            // arrange
            _mock.Setup(y => y.GetByIdAsync(FakeData().Id)).ReturnsAsync(FakeData());
            // act
            var actionresult = _articlecontroller.Get(Guid.NewGuid());
            var okobjectresult = actionresult.Result as BadRequestObjectResult;
            // assert
            Assert.Equal(okobjectresult.StatusCode, (int)HttpStatusCode.BadRequest);

        }
        
        [Fact]
        public void GetAll_Returns_Correctly()
        {
            // arrange
            _mock.Setup(y => y.GetAllAsync());
            // act
            var actionresult = _articlecontroller.GetAll();
            var okobjectresult = actionresult.Result as OkObjectResult;

            // assert
            Assert.Equal(okobjectresult.StatusCode, (int)okobjectresult.StatusCode);

        }
        [Fact]
        public async Task Create_Return_Correctly()
        {
            // arrange
            var added = FakeData();
            

            _mockuser.Setup(x => x.GetByIdAsync(added.AuthorId))
                            .ReturnsAsync(new User());

            _mockmagazine.Setup(x => x.GetByIdAsync(added.MagazineId))
                                .ReturnsAsync(new Magazine());

            _mock.Setup(x => x.GetByIdAsync(added.Id))
                               .ReturnsAsync(null as Article);
            
            _mock.Setup(x => x.AddAsync(added))
                               .ReturnsAsync(added);

            // act
            //var mappedarticle = mapper.Map<ArticleResponse>(articlemodel);
            var articlemodel = mapper.Map<ArticleModel>(added);
            var actionresult = _articlecontroller.Create(articlemodel);
            var okobjectresult = actionresult.Result as OkObjectResult;

            // assert
            Assert.IsType<OkObjectResult>(okobjectresult);
        }

        [Fact]
        public void Update_Return_Correctly()
        {

            // arrange
            var articletoupdate = FakeData();
            articletoupdate.Content = "this is updated content";
            _mock.Setup(y => y.GetByIdAsync(FakeData().Id)).ReturnsAsync(FakeData());
            _mock.Setup(y => y.UpdateAsync(articletoupdate)).ReturnsAsync(articletoupdate);
            ArticleModel updatedarticlemodel = new ArticleModel { Id = FakeData().Id, Content = "this is updated content" };

            // act
            var actionresult = _articlecontroller.Update(FakeData().Id, updatedarticlemodel);
            var okobjectresult = actionresult.Result as OkObjectResult;

            // assert
            Assert.IsType<OkObjectResult>(okobjectresult);


        }
        [Fact]
        public void Delete_Return_Correctly()
        {
            // arrange
            _mock.Setup(y => y.GetByIdAsync(FakeData().Id)).ReturnsAsync(FakeData());
            // act
            var actionresult = _articlecontroller.Delete(FakeData().Id).Result;

            // assert
            Assert.IsType<OkObjectResult>(actionresult);

        }
        [Fact]
        public void Delete_Returns_NotFound_InvalidId()
        {
            // arrange
            _mock.Setup(y => y.GetByIdAsync(FakeData().Id)).ReturnsAsync(FakeData());
            // act
            var actionresult = _articlecontroller.Delete(Guid.NewGuid()).Result;
            // assert
            Assert.IsType<BadRequestObjectResult>(actionresult);
        }
        public Article FakeData()
        {
            return new Article
            {
                Id = Guid.Parse("d9227df2-6924-47b3-bd81-b4ed46e5568c"),
                Title = "test",
                Content= "test",
                MagazineId = Guid.Parse("7bde6cdd-bdeb-443a-b040-b058588ee8bb"),
                AuthorId = Guid.Parse("d2c7cf8e-9545-4cfd-969e-a67766f71c82")
            };
        }
    }
}
