using Microsoft.AspNetCore.Mvc;
using BookProject.Application.Interfaces;
using System.Threading.Tasks;
using BookProject.Application.Models;
using BookProject.Application.Mapper;
using AutoMapper;
using BookProject.Application.Validation;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IMapper mapper = BookProjectMapper.Mapper;
        private readonly IArticleService _articleService;
        private readonly IOrderService _orderService;
        private readonly OrderValidator _orderValidator;
        private readonly IUserService _userService;
        public OrderController(IOrderService orderservice,IUserService userservice, IArticleService articleService)
        {
            _orderService = orderservice;
            _userService = userservice;
            _articleService = articleService;
            _orderValidator = new OrderValidator(_orderService,_articleService,_userService);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if(id <= 0)
            {
                return BadRequest("Invalid Order ID");
            }
            var order = await _orderService.GetByIdAsync(id);

            if(order == null)
            {
                return NotFound();
            }
            var orderModel = mapper.Map<OrderModel>(order);
            return Ok(orderModel);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderService.GetAllAsync();
            var orderResponses = mapper.Map<IEnumerable<OrderModel>>(orders);
            return Ok(orderResponses);
        }
        [HttpPost]
        public async Task<IActionResult> Create(OrderModel order)
        {
            if(order == null)
            {
                return BadRequest("Order information is missing");
            }
            var validationResult = await _orderValidator.ValidateAsync(order);
            if(!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var newOrder = await _orderService.AddAsync(order);

            return Ok(newOrder);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OrderModel order)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var existingOrder = await _orderService.GetByIdAsync(id);
            if(existingOrder == null)
            {
                return NotFound();
            }
            var updatedOrderModel = new OrderModel
            {
                Id=existingOrder.Id,
                ArticleId=order.ArticleId,
                UserId=order.UserId,
                CreatedDate=order.CreatedDate
            };

            var updatedOrder = await _orderService.UpdateAsync(updatedOrderModel);

            return Ok(updatedOrder);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingOrder = await _orderService.GetByIdAsync(id);
            if(existingOrder == null)
            {
                return BadRequest("Error. Invalid ID!");
            }

            await _orderService.DeleteAsync(id);

            return Ok("Deleted");
        }
    }
}
