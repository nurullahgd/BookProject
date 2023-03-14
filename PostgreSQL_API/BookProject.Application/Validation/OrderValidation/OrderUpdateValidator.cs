using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using System.Text;
using System.Threading.Tasks;
using BookProject.Data.Entities;
using BookProject.Application.Interfaces;
using BookProject.Application.Models;

namespace BookProject.Application.Validation.OrderValidation
{
    public class OrderUpdateValidator:AbstractValidator<OrderModel>
    {
        private readonly IOrderService _orderService;
        private readonly IArticleService _articleService;
        private readonly IUserService _userService;
        public OrderUpdateValidator(IOrderService orderService, IArticleService articleService, IUserService userService)
        {
            _orderService = orderService;
            _articleService = articleService;
            _userService = userService;
            _orderService = orderService;

            RuleFor(o => o.ArticleId).NotEmpty().WithMessage("Article Id is required")
                .GreaterThan(0).WithMessage("Id must be greater than 0")
                .MustAsync(async (id, cancellationToken) =>
                {
                    return await _articleService.GetByIdAsync(id) != null;
                }).WithMessage("Article does not exist.");

            RuleFor(o => o.UserId).NotEmpty().WithMessage("User Id is required")
                .GreaterThan(0).WithMessage("Id must be greater than 0")
                .MustAsync(async (id, cancellationToken) =>
                {
                    return await _userService.GetByIdAsync(id) != null;
                }).WithMessage("Author does not exist.");

            RuleFor(o => o.CreatedDate).NotEmpty().WithMessage("CreatedDate is required.");

        }
    }
}
