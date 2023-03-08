using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookProject.Data.Entities;

namespace BookProject.Application.Interfaces
{
    public interface IMagazineService
    {
        Task<Magazine> GetByIdAsync(int id);
        Task<IEnumerable<Magazine>> GetAllAsync();
        Task<Magazine> AddAsync(Magazine user);
        Task<Magazine> UpdateAsync(Magazine user);
        Task<Magazine> DeleteAsync(int id);
    }
}
