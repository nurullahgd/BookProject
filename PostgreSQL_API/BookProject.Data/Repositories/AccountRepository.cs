using BookProject.Data;
using BookProject.Data.Entities;
using BookProject.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookProject.Data.Repositories
{

    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        private readonly PostgreSqlExampleDbContext _context;
        public AccountRepository(PostgreSqlExampleDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Account> FindUsernameAndPassword(string username, string password)
        {

            var account = _context.Set<Account>().FirstOrDefaultAsync(x => x.Username == username).Result;

            if(account != null && account.Password == password)
            {
                return account;
            }
            return null;
        }
        public async Task<Account> GetbyNameAsync(string username)
        {
            var check = await _context.Set<Account>().SingleOrDefaultAsync(x => x.Username == username);
            if(check != null)
            {
                return check;
            }
            return null;
        }


    }


    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> FindUsernameAndPassword(string username, string password);
        Task<Account> GetbyNameAsync(string username);
    }

}
