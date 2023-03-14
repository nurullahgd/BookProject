﻿using BookProject.Data.Repositories;
using System.Collections.Generic;
namespace BookProject.Data.Entities
{
    public class User
    {
        
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}
