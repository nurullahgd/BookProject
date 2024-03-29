﻿using BookProject.Application.Interfaces;
using BookProject.Application.Models;
using BookProject.Data.Entities;
using BookProject.Data.Repositories;
using System;
using System.Collections.Generic;
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

        public async Task<Order> GetByIdAsync(Guid id)
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
                
                ArticleId = orderModel.ArticleId,
                CreatedDate = orderModel.CreatedDate,
                AccountId = orderModel.AccountId
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
            if(orderModel.AccountId==null)
            {
                order.AccountId = order.AccountId;
            }
            else
            {
                order.AccountId = orderModel.AccountId;
            }
            if(orderModel.ArticleId==null)
            {
                order.ArticleId = order.ArticleId;
            }
            else
            {
                order.ArticleId = orderModel.ArticleId;
            }
            return await _orderRepository.UpdateAsync(order);
        }

        public async Task<Order> DeleteAsync(Guid id)
        {
            return await _orderRepository.DeleteAsync(id);
        }
    }
}
