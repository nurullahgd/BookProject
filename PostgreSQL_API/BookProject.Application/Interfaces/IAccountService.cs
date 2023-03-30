using BookProject.Application.Models;
using BookProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookProject.Application.Interfaces
{
    public interface IAccountService
    {
        Task<Account> GetByIdAsync(Guid id);
        Task<IEnumerable<Account>> GetAllAsync();
        Task<Account> AddAsync(AccountResponse accountResponse);
        Task<Account> UpdateAsync(AccountModel accountModel);
        Task<Account> DeleteAsync(Guid id);
        Task<Account> FindPassword(string username,string password);
        Task<Account> GetByNameAsync(string username);
        Task<Account> Register(Account accountResponse);
    }
}
