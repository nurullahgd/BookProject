using System.Text;
using System.Threading.Tasks;
using BookProject.Application.Interfaces;
using BookProject.Data.Repositories;
using BookProject.Application.Services;
using BookProject.Data;
using BookProject.Data.Entities;
using System.Collections.Generic;
using BookProject.Application.Models;

namespace BookProject.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> AddAsync(UserModel userModel)
        {
            var user = new User
            {
                Id = userModel.Id,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email
                
            };

            return await _userRepository.AddAsync(user);
        }

        public async Task<User> UpdateAsync(UserModel userModel)
        {
            var user = await _userRepository.GetByIdAsync(userModel.Id);

            if(user == null)
            {
                // Kullanıcı bulunamadı
                return null;
            }
            user.FirstName = userModel.FirstName ?? user.FirstName;
            user.LastName = userModel.LastName ?? user.LastName;
            user.Email = userModel.Email ?? user.Email;

            return await _userRepository.UpdateAsync(user);
        }

        public async Task<User> DeleteAsync(int id)
        {
            return await _userRepository.DeleteAsync(id);
        }
    }
}
