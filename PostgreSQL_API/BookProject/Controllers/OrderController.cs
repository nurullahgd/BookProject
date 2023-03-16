using Microsoft.AspNetCore.Mvc;
using BookProject.Application.Interfaces;
using System.Threading.Tasks;
using BookProject.Application.Models;
using BookProject.Application.Mapper;
using AutoMapper;
using BookProject.Application.Validation.OrderValidation;
using System.Collections.Generic;
using System;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class OrderController : ControllerBase
    {
        IMapper mapper = BookProjectMapper.Mapper;
        private readonly IArticleService _articleService;
        private readonly IOrderService _orderService;
        private readonly OrderAddValidator _orderAddValidator;
        private readonly OrderUpdateValidator _orderUpdateValidator;
        private readonly IUserService _userService;
        public OrderController(IOrderService orderservice,IUserService userservice, IArticleService articleService)
        {
            _orderService = orderservice;
            _userService = userservice;
            _articleService = articleService;
            _orderAddValidator = new OrderAddValidator(_orderService,_articleService,_userService);
            _orderUpdateValidator= new OrderUpdateValidator(_orderService, _articleService, _userService);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            
            var order = await _orderService.GetByIdAsync(id);

            if(order == null)
            {
                return BadRequest("Invalid Order ID");
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
        public async Task<IActionResult> Create(OrderResponse order)
        {
            if(order == null)
            {
                return BadRequest("Order information is missing");
            }
            var validationResult = await _orderAddValidator.ValidateAsync(order);
            if(!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var newOrder = await _orderService.AddAsync(order);
            //var orderResponse = mapper.Map<OrderModel>(newOrder);
            return Ok(newOrder);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, OrderModel order)
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
                AccountId=order.AccountId,
                CreatedDate=order.CreatedDate
            };
            var validationResult = await _orderUpdateValidator.ValidateAsync(order);
            if(!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var updatedOrder = await _orderService.UpdateAsync(updatedOrderModel);
            var orderResponse = mapper.Map<OrderModel>(updatedOrder);

            return Ok(orderResponse);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
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
