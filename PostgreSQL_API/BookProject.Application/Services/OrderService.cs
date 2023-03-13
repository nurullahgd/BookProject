using BookProject.Application.Interfaces;
using BookProject.Application.Models;
using BookProject.Data.Entities;
using BookProject.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookProject.Application.Services
{
    public class OrderService :IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Order> AddAsync(OrderModel orderModel)
        {
            var order = new Order
            {
                Id = orderModel.Id,
                ArticleId = orderModel.ArticleId,
                CreatedDate = orderModel.CreatedDate,
                UserId = orderModel.UserId
            };

            return await _orderRepository.AddAsync(order);
        }

        public async Task<Order> UpdateAsync(OrderModel orderModel)
        {
            var order = await _orderRepository.GetByIdAsync(orderModel.Id);

            if(order == null)
            {
                // Kullanıcı bulunamadı
                return null;
            }
            if(orderModel.ArticleId != 0)
            {
                order.ArticleId = orderModel.ArticleId;
            }
            else
            {
                order.ArticleId = order.ArticleId;
            }

            if(orderModel.CreatedDate !=default)
            {
                order.CreatedDate = orderModel.CreatedDate;
            }
            else
            {
                order.CreatedDate = order.CreatedDate;
            }
            if(orderModel.UserId!=0)
            {
                order.UserId = orderModel.UserId;
                
            }
            else
            {
                order.UserId = order.UserId;
            }
            return await _orderRepository.UpdateAsync(order);
        }

        public async Task<Order> DeleteAsync(int id)
        {
            return await _orderRepository.DeleteAsync(id);
        }
    }
}
