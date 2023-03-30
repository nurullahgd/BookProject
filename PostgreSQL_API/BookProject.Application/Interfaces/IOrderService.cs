using BookProject.Application.Models;
using BookProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookProject.Application.Interfaces
{
    public interface IOrderService
    {
        Task<Order> GetByIdAsync(Guid id);
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order> AddAsync(OrderModel user);
        Task<Order> UpdateAsync(OrderModel user);
        Task<Order> DeleteAsync(Guid id);
    }
}
