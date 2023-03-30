using BookProject.Data.Entities;
using Microsoft.EntityFrameworkCore;
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
        public async Task<Account> FindPassword(string username, string password)
        {

            var account = _context.Set<Account>().FirstOrDefaultAsync(x => x.Username == username).Result;
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(password, account.Password);
            if(isValidPassword)
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
        public async Task<Account> Register(Account accountResponse)
        {
            var check = await _context.Set<Account>().SingleOrDefaultAsync(x => x.Username == accountResponse.Username);
            if(check != null) return accountResponse;
            return null;
            
        }


    }


    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> FindPassword(string username, string password);
        Task<Account> GetbyNameAsync(string username);
        Task<Account> Register(Account accountResponse);
    }

}
