using BookProject.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookProject.Data.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(DbContext context) : base(context)
        {

        }
    }

    public interface IOrderRepository : IRepository<Order>
    {

    }
}
