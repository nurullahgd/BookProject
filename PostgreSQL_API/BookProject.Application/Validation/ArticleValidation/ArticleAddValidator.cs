using FluentValidation;
using BookProject.Application.Interfaces;
using BookProject.Application.Models;

namespace BookProject.Application.Validation.ArticleValidation
{
    public class ArticleAddValidator : AbstractValidator<ArticleModel>
    {
        private readonly IArticleService _articleService;
        private readonly IUserService _userService;
        private readonly IMagazineService _magazineService;

        public ArticleAddValidator(IUserService userService, IArticleService articleService, IMagazineService magazineService)
        {
            _userService = userService;
            _articleService = articleService;
            _magazineService = magazineService;

            RuleFor(a => a.Title).NotEmpty().WithMessage("Title is Required");

            RuleFor(a => a.MagazineId)
                .NotEmpty().WithMessage("Magazine is required")
                .MustAsync(async (id, cancellationToken) =>
                {
                    var magazine = await _magazineService.GetByIdAsync(id);
                    return magazine != null;
                }).WithMessage("Magazine not found");

            RuleFor(a => a.AuthorId)
                .NotEmpty().WithMessage("Author is required")
                .MustAsync(async (id, cancellationToken) =>
                {
                    var magazine = await _userService.GetByIdAsync(id);
                    return magazine != null;
                }).WithMessage("Author not found");
        }
    }
}
