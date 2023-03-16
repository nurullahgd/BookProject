using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using System.Text;
using System.Threading.Tasks;
using BookProject.Data.Entities;
using BookProject.Application.Interfaces;
using BookProject.Application.Models;


namespace BookProject.Application.Validation.ArticleValidation
{
    public class ArticleUpdateValidator: AbstractValidator<ArticleModel>
    {
        private readonly IArticleService _articleService;
        private readonly IUserService _userService;
        private readonly IMagazineService _magazineService;
        public ArticleUpdateValidator(IUserService userService, IArticleService articleService, IMagazineService magazineService)
        {
            _userService = userService;
            _articleService = articleService;
            _magazineService = magazineService;

            RuleFor(a => a.Title).NotEmpty().WithMessage("Title is Required");

            RuleFor(a => a.MagazineId).NotEmpty().WithMessage("Magazine is required");

            RuleFor(a => a.AuthorId).NotEmpty().WithMessage("Author is required");

        }
    }
}
