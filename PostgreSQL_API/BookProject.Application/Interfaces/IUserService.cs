using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookProject.Application.Models;
using BookProject.Data.Entities;

namespace BookProject.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> GetByIdAsync(Guid id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> AddAsync(UserResponse user);
        Task<User> UpdateAsync(UserModel user);
        Task<User> DeleteAsync(Guid id);
        

    }
}
