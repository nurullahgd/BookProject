using BookProject.Application.Interfaces;
using BookProject.Application.Models;
using FluentValidation;

namespace BookProject.Application.Validation.UserValidation
{
    public class UserAddValidator : AbstractValidator<UserResponse>
    {
        private readonly IUserService _userService;
        public UserAddValidator(IUserService userService)
        {
            _userService = userService;

            RuleFor(u => u.FirstName).NotEmpty().WithMessage("First name is required.").MinimumLength(3).WithMessage("Fist name must be at least 3 characters.");
            RuleFor(u => u.LastName).NotEmpty().WithMessage("Last name is required.");
            RuleFor(u => u.Email).NotEmpty().WithMessage("Email is required.").EmailAddress().WithMessage("Email is not valid.");
        }
            
    }
}
