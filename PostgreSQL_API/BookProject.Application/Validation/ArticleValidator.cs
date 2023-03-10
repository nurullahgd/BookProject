using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using System.Text;
using System.Threading.Tasks;
using BookProject.Data.Entities;
using BookProject.Application.Interfaces;
using BookProject.Application.Models;

namespace BookProject.Application.Validation
{
    public class ArticleValidator :AbstractValidator<ArticleModel>
    {
        private readonly IArticleService _articleService;
        private readonly IUserService _userService;
        private readonly IMagazineService _magazineService;
        public ArticleValidator(IUserService userService,IArticleService articleService,IMagazineService magazineService)
        {
            _userService = userService;
            _articleService = articleService;
            _magazineService = magazineService;

            RuleFor(a => a.Title).NotEmpty().WithMessage("Title is Required");
            RuleFor(a => a.MagazineId).NotEmpty().WithMessage("Magazine is Required")
                .GreaterThan(0).WithMessage("MagazineId must be greater than 0!")
                .MustAsync(async (id, cancellationToken) =>
                {
                    return await _magazineService.GetByIdAsync(id)!=null;
                }).WithMessage("Magazine does not exist.");
            RuleFor(a => a.AuthorId).NotEmpty().WithMessage("Author is required")
                .GreaterThan(0).WithMessage("AuthorId must be greater than 0!")
                .MustAsync(async(id, cancellationToken) =>
                {
                    return await _userService.GetByIdAsync(id) != null;
                }).WithMessage("Author does not exist.");
            RuleFor(a => a.id).NotEmpty().WithMessage("Id is required!")
            .GreaterThan(0).WithMessage("Id must be greater than 0!")
                .MustAsync(async (id, cancellationToken) =>
                {
                    var magazine = await _articleService.GetByIdAsync(id);
                    return magazine == null;
                }).WithMessage("A magazine with the same id already exist.");
        }
    }
}
