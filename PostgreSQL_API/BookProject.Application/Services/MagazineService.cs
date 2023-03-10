using System.Collections.Generic;
using System.Threading.Tasks;
using BookProject.Application.Interfaces;
using BookProject.Data.Repositories;
using BookProject.Data.Entities;
using BookProject.Application.Models;

namespace BookProject.Application.Services
{
    public class MagazineService : IMagazineService
    {
        private readonly IMagazineRepository _magazineRepository;

        public MagazineService(IMagazineRepository magazineRepository)
        {
            _magazineRepository = magazineRepository;
        }

        public async Task<Magazine> GetByIdAsync(int id)
        {
            return await _magazineRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Magazine>> GetAllAsync()
        {
            return await _magazineRepository.GetAllAsync();
        }

        public async Task<Magazine> AddAsync(MagazineModel magazineModel)
        {
            var magazine = new Magazine
            {
                Id = magazineModel.Id,
                Name = magazineModel.Name
            };
            return await _magazineRepository.AddAsync(magazine);

        }

        public async Task<Magazine> UpdateAsync(MagazineModel magazineModel)
        {
            var magazine = await _magazineRepository.GetByIdAsync(magazineModel.Id);

            if(magazine == null)
            {
                // Kullanıcı bulunamadı
                return null;
            }
            magazine.Name = magazineModel.Name ?? magazine.Name;

            
            return await _magazineRepository.UpdateAsync(magazine);
        }

        public async Task<Magazine> DeleteAsync(int id)
        {
            return await _magazineRepository.DeleteAsync(id);
        }
    }
}
