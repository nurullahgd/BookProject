using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using System.Text;
using System.Threading.Tasks;
using BookProject.Data.Entities;
using BookProject.Application.Interfaces;
using BookProject.Application.Models;

namespace BookProject.Application.Validation.AccountValidation
{
    public class AccountValidator: AbstractValidator<AccountResponse>
    {
        private readonly IAccountService _accountService;
        public AccountValidator(IAccountService accountService)
        {
            _accountService = accountService;
            RuleFor(a => a.Username).NotEmpty().WithMessage("Username is required")
                .MinimumLength(3).WithMessage("Minimum Lenght 3!")
                .MustAsync(async (username, cancellationToken) =>
                {
                    var account = await _accountService.GetByNameAsync(username);
                    return account == null;
                }).WithMessage("Username is already taken");

            RuleFor(a => a.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}
