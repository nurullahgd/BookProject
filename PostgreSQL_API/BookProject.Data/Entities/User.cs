using System;
using System.Collections.Generic;
namespace BookProject.Data.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}
