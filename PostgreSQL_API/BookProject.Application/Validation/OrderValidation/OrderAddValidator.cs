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
    public class OrderAddValidator: AbstractValidator<OrderResponse>
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

            RuleFor(o => o.ArticleId).NotEmpty().WithMessage("Article Id is required");

            RuleFor(o => o.AccountId).NotEmpty().WithMessage("User Id is required");

            RuleFor(o => o.CreatedDate).NotEmpty().WithMessage("CreatedDate is required.");

        }
    }
}
