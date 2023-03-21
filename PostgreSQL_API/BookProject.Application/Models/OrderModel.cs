using System;

namespace BookProject.Application.Models
{
    public class OrderModel
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public Guid ArticleId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
