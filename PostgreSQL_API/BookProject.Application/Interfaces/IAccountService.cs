using BookProject.Application.Models;
using BookProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookProject.Application.Interfaces
{
    public interface IAccountService
    {
        Task<Account> GetByIdAsync(Guid id);
        Task<IEnumerable<Account>> GetAllAsync();
        Task<Account> AddAsync(AccountResponse article);
        Task<Account> UpdateAsync(AccountModel article);
        Task<Account> DeleteAsync(Guid id);
        Task<Account> FindUsernameAndPassword(string username,string password);
        Task<Account> GetByNameAsync(string username);
    }
}
