using FluentValidation;
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

            
            RuleFor(u => u.Email).EmailAddress().WithMessage("Email is not valid.");
        }
    }
}
