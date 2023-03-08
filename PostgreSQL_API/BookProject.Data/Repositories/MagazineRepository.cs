using BookProject.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
