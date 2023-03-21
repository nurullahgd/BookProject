using System;

namespace BookProject.Application.Models
{
    public class OrderResponse
    {
        public Guid AccountId { get; set; }
        public Guid ArticleId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
