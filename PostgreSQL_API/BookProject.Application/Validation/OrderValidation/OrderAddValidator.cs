using BookProject.Application.Interfaces;
using BookProject.Application.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookProject.Application.Validation.OrderValidation
{
    public class OrderAddValidator: AbstractValidator<OrderModel>
    {
        private readonly IOrderService _orderService;
        private readonly IArticleService _articleService;
        private readonly IUserService _userService;
        public OrderAddValidator(IOrderService orderService, IArticleService articleService, IUserService userService)
        {
            _orderService = orderService;
            _articleService = articleService;
            _userService = userService;
            _orderService = orderService;

            RuleFor(o => o.Id).NotEmpty().WithMessage("Id is required")
                .GreaterThan(0).WithMessage("Id must be greater than 0")
                .MustAsync(async (id, cancellationToken) =>
                {
                    var order = await _articleService.GetByIdAsync(id);
                    return order == null;
                }).WithMessage("A Order with the same id already exists.");

            RuleFor(o=>o.ArticleId).NotEmpty().WithMessage("Article Id is required")
                .GreaterThan(0).WithMessage("ArticleId must be greater than 0")
                .MustAsync(async (id, cancellationToken) =>
                {
                    return await _articleService.GetByIdAsync(id) != null;
                }).WithMessage("Article does not exist.");

            RuleFor(o => o.UserId).NotEmpty().WithMessage("User Id is required")
                .GreaterThan(0).WithMessage("ArticleId must be greater than 0")
                .MustAsync(async (id, cancellationToken) =>
                {
                    return await _userService.GetByIdAsync(id) != null;
                }).WithMessage("Author does not exist.");

            RuleFor(o => o.CreatedDate).NotEmpty().WithMessage("CreatedDate is required.");

        }
    }
}
