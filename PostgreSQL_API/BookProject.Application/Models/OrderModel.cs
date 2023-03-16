using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
