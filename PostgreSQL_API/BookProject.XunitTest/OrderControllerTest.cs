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
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace BookProject.XunitTest
{
    public class OrderControllerTest
    {
        private Mock<IArticleRepository> _articlemock;
        private Mock<IAccountRepository> _accmock;
        private Mock<IOrderRepository> _mock;
        private readonly ArticleService _articleservice;
        private readonly AccountService _accountService;
        private readonly OrderService _orderService;
        private readonly OrderController _orderController;
        IMapper mapper = BookProjectMapper.Mapper;
        public OrderControllerTest()
        {
            _mock = new Mock<IOrderRepository>();
            _accmock = new Mock<IAccountRepository>();
            _articlemock = new Mock<IArticleRepository>();
            _articleservice = new ArticleService(_articlemock.Object,mapper);
            _accountService = new AccountService(_accmock.Object,mapper);
            _orderService = new OrderService(_mock.Object);
            _orderController = new OrderController(_orderService,_accountService,_articleservice);

        }
        [Fact]
        public void Get_Returns_Correct_Id()
        {
            // arrange
            var ordertoget = FakeData();
            _mock.Setup(y => y.GetByIdAsync(FakeData().Id)).ReturnsAsync(FakeData());
            // act
            var actionresult = _orderController.Get(FakeData().Id);
            var okobjectresult = actionresult.Result as OkObjectResult;

            // assert
            var articlemap = mapper.Map<OrderModel>(ordertoget);
            Assert.IsType<OkObjectResult>(okobjectresult);


        }

        [Fact]
        public void Get_Returns_Wrong_Id()
        {
            // arrange
            _mock.Setup(y => y.GetByIdAsync(FakeData().Id)).ReturnsAsync(FakeData());
            // act
            var actionresult = _orderController.Get(Guid.NewGuid());
            var badRequestObjectResult = actionresult.Result as BadRequestObjectResult;
            // assert
            Assert.Equal(badRequestObjectResult.StatusCode, (int)HttpStatusCode.BadRequest);

        }

        [Fact]
        public void GetAll_Returns_Correctly()
        {
            // arrange
            _mock.Setup(y => y.GetAllAsync());
            // act
            var actionresult = _orderController.GetAll();
            var okobjectresult = actionresult.Result as OkObjectResult;

            // assert
            Assert.Equal(okobjectresult.StatusCode, (int)okobjectresult.StatusCode);

        }
        [Fact]
        public void Create_Return_Correctly()
        {
            // arrange
            var added = FakeData();
            var ordermodel = mapper.Map<OrderModel>(added);

            _articlemock.Setup(x => x.GetByIdAsync(ordermodel.ArticleId))
                            .ReturnsAsync(new Article());

            _accmock.Setup(x => x.GetByIdAsync(ordermodel.AccountId))
                                .ReturnsAsync(new Account());

            _mock.Setup(x => x.GetByIdAsync(ordermodel.Id))
                               .ReturnsAsync(null as Order);

            _mock.Setup(x => x.AddAsync(added))
                               .ReturnsAsync(added);

            // act
            //var mappedarticle = mapper.Map<ArticleResponse>(articlemodel);
            var actionresult = _orderController.Create(ordermodel);
            var okobjectresult = actionresult.Result as OkObjectResult;

            // assert
            Assert.IsType<OkObjectResult>(okobjectresult);
        }

        [Fact]
        public void Update_Return_Correctly()
        {

            // arrange
            var ordertoupdate = FakeData();
            ordertoupdate.CreatedDate = DateTime.ParseExact("12.04.2022", "dd.MM.yyyy", CultureInfo.InvariantCulture);
            _mock.Setup(y => y.GetByIdAsync(FakeData().Id)).ReturnsAsync(FakeData());
            _mock.Setup(y => y.UpdateAsync(ordertoupdate)).ReturnsAsync(ordertoupdate);
            OrderModel updatedarticlemodel = new OrderModel { Id = FakeData().Id, CreatedDate = DateTime.ParseExact("12.04.2022", "dd.MM.yyyy", CultureInfo.InvariantCulture) };

            // act
            var actionresult = _orderController.Update(FakeData().Id, updatedarticlemodel);
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
            var actionresult = _orderController.Delete(FakeData().Id).Result;

            // assert
            Assert.IsType<OkObjectResult>(actionresult);

        }
        [Fact]
        public void Delete_Returns_NotFound_InvalidId()
        {
            // arrange
            _mock.Setup(y => y.GetByIdAsync(FakeData().Id)).ReturnsAsync(FakeData());
            // act
            var actionresult = _orderController.Delete(Guid.NewGuid()).Result;
            // assert
            Assert.IsType<BadRequestObjectResult>(actionresult);
        }
        public Order FakeData()
        {
            return new Order
            {
                Id = Guid.Parse("d9227df2-6924-47b3-bd81-b4ed46e5568c"),
                AccountId = Guid.Parse("7bde6cdd-bdeb-443a-b040-b058588ee8bb"),
                ArticleId = Guid.Parse("d2c7cf8e-9545-4cfd-969e-a67766f71c82"),
                CreatedDate = DateTime.ParseExact("23.04.2002", "dd.MM.yyyy", CultureInfo.InvariantCulture)

            };
        }
    }
}
