using BookProject.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookProject.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {

        }
    }

    public interface IUserRepository : IRepository<User>
    {

    }
}
