using AutoMapper;
using BookProject.Application.Interfaces;
using BookProject.Application.Models;
using BookProject.Data.Entities;
using BookProject.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookProject.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;


        public AccountService(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }
        public async Task<Account> FindUsernameAndPassword(string username, string password)
        {
            return await _accountRepository.FindUsernameAndPassword(username, password);
        }
        public async Task<Account> GetByNameAsync(string username)
        {
            return await _accountRepository.GetbyNameAsync(username);
        }

        public async Task<Account> GetByIdAsync(Guid id)
        {
            return await _accountRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await _accountRepository.GetAllAsync();
        }

        //public async Task<Account> Login(AccountResponse accountResponse)
        //{
        //    var account = _accountRepository.FindUsernameAndPassword(accountResponse.Username,accountResponse.Password);
        //    bool isValidPassword = BCrypt.Net.BCrypt.Verify(account.pas, accountResponse.Password);
        //}
        public async Task<Account> Register(Account account)
        {

            return await _accountRepository.Register(account);
        }
        public async Task<Account> AddAsync(AccountResponse accountResponse)
        {
            //accountResponse.Password= BCrypt.Net.BCrypt.HashPassword(accountResponse.Password);
            var account = new Account
            {
                Username = accountResponse.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(accountResponse.Password)
            };

            return await _accountRepository.AddAsync(account);
        }

        public async Task<Account> UpdateAsync(AccountModel accountModel)
        {
            var account = await _accountRepository.GetByIdAsync(accountModel.Id);

            if(account == null)
            {
                // Kullanıcı bulunamadı
                return null;
            }

            account.Username = accountModel.Username ?? account.Username;
            account.Password = accountModel.Password ?? account.Password;
            //article.MagazineId = articleModel.MagazineId ?? article.MagazineId;

            return await _accountRepository.UpdateAsync(account);
        }

        public async Task<Account> DeleteAsync(Guid id)
        {
            return await _accountRepository.DeleteAsync(id);

        }
    }
}
