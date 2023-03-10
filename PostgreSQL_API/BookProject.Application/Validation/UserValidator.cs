using BookProject.Application.Interfaces;
using BookProject.Application.Models;
using BookProject.Data.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookProject.Application.Validation
{
    public class UserValidator : AbstractValidator<UserModel>
    {
        private readonly IUserService _userService;
        public UserValidator(IUserService userService)
        {
            _userService = userService;

            RuleFor(u => u.Id).NotEmpty().WithMessage("ID name is required.")
                .GreaterThan(0).WithMessage("Id must be greater than 0")
                .MustAsync(async (id, cancellationToken) =>
                {
                    var user = await _userService.GetByIdAsync(id);
                    return user == null;
                }).WithMessage("A user with the same id already exists.");

            RuleFor(u => u.FirstName).NotEmpty().WithMessage("First name is required.").MinimumLength(3).WithMessage("Fist name must be at least 3 characters.");
            RuleFor(u => u.LastName).NotEmpty().WithMessage("Last name is required.");
            RuleFor(u => u.Email).NotEmpty().WithMessage("Email is required.").EmailAddress().WithMessage("Email is not valid.");
        }
            
    }
}
