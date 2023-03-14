using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using System.Text;
using System.Threading.Tasks;
using BookProject.Data.Entities;
using BookProject.Application.Interfaces;
using BookProject.Application.Models;

namespace BookProject.Application.Validation.UserValidation
{
    public class UserUpdateValidator:AbstractValidator<UserModel>
    {
        private readonly IUserService _userService;
        public UserUpdateValidator(IUserService userService)
        {
            _userService = userService;

            RuleFor(u => u.Id).NotEmpty().WithMessage("ID name is required.")
                .GreaterThan(0).WithMessage("Id must be greater than 0");
            RuleFor(u => u.FirstName).NotEmpty().WithMessage("First name is required.").MinimumLength(3).WithMessage("Fist name must be at least 3 characters.");
            RuleFor(u => u.LastName).NotEmpty().WithMessage("Last name is required.");
            RuleFor(u => u.Email).NotEmpty().WithMessage("Email is required.").EmailAddress().WithMessage("Email is not valid.");
        }
    }
}
