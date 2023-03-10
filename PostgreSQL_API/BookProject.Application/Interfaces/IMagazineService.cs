using System.Collections.Generic;
using System.Threading.Tasks;
using BookProject.Application.Models;
using BookProject.Data.Entities;

namespace BookProject.Application.Interfaces
{
    public interface IMagazineService
    {
        Task<Magazine> GetByIdAsync(int id);
        Task<IEnumerable<Magazine>> GetAllAsync();
        Task<Magazine> AddAsync(MagazineModel magazine);
        Task<Magazine> UpdateAsync(MagazineModel magazine);
        Task<Magazine> DeleteAsync(int id);
    }
}
