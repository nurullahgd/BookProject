using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookProject.Application.Interfaces;
using BookProject.Data.Repositories;
using BookProject.Application.Services;
using BookProject.Data;
using BookProject.Data.Entities;

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

        public async Task<Magazine> AddAsync(Magazine magazine)
        {
            return await _magazineRepository.AddAsync(magazine);
        }

        public async Task<Magazine> UpdateAsync(Magazine magazine)
        {
            return await _magazineRepository.UpdateAsync(magazine);
        }

        public async Task<Magazine> DeleteAsync(int id)
        {
            return await _magazineRepository.DeleteAsync(id);
        }
    }
}
