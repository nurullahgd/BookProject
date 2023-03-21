using BookProject.Application.Interfaces;
using BookProject.Application.Models;
using FluentValidation;

namespace BookProject.Application.Validation.OrderValidation
{
    public class OrderAddValidator: AbstractValidator<OrderModel>
    {
        private readonly IOrderService _orderService;
        private readonly IArticleService _articleService;
        private readonly IAccountService _accountService;
        public OrderAddValidator(IOrderService orderService, IArticleService articleService, IAccountService accountService)
        {
            _orderService = orderService;
            _articleService = articleService;
            _accountService = accountService;
            _orderService = orderService;

            RuleFor(o => o.ArticleId).NotEmpty().WithMessage("Article Id is required")
                .MustAsync(async (id, cancellationToken) =>
                {
                    var magazine = await _articleService.GetByIdAsync(id);
                    return magazine != null;
                }).WithMessage("Article not found"); 

            RuleFor(o => o.AccountId).NotEmpty().WithMessage("User Id is required")
                .MustAsync(async (id, cancellationToken) =>
                {
                    var magazine = await _accountService.GetByIdAsync(id);
                    return magazine != null;
                }).WithMessage("Account not found");

            RuleFor(o => o.CreatedDate).NotEmpty().WithMessage("CreatedDate is required.");

        }
    }
}
