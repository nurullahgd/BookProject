using BookProject.Application.Interfaces;
using BookProject.Application.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookProject.Application.Validation.AccountValidation
{
    public class AccountLoginValidation: AbstractValidator<AccountResponse>
    {
        private readonly IAccountService _accountService;
        public AccountLoginValidation(IAccountService accountService)
        {
            _accountService = accountService;

            RuleFor(a => a.Username).NotEmpty().WithMessage("Username is required")
                .MustAsync(async (username, cancellationToken) =>
                 {
                     var account = await _accountService.GetByNameAsync(username);
                     return account != null;
                 }).WithMessage("User not found");
            RuleFor(a => a.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}
