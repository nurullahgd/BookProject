using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookProject.Data.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public Guid ArticleId { get; set; }
        public DateTime CreatedDate { get; set; }
        public Account Account { get; set; }
        public Article Article { get; set; }
    }
}
