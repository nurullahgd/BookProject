using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookProject.Data.Entities
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Username { get; set; } 
        public string Password { get; set; } 
        public ICollection<Order> Orders { get; set; }
    }
}
