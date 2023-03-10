using BookProject.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookProject.Data.Repositories
{
    public class MagazineRepository : BaseRepository<Magazine>, IMagazineRepository
    {
        public MagazineRepository(DbContext context) : base(context)
        {

        }
    }

    public interface IMagazineRepository : IRepository<Magazine>
    {

    }
    
}
