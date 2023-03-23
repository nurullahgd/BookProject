using System;

namespace BookProject.Application.Models
{
    public class ArticleResponseNames
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string MagazineName { get; set; }
        public string AuthorName { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
