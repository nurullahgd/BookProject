
using System;
using System.Collections.Generic;

namespace BookProject.Data.Entities
{
    public class Magazine
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}
