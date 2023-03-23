using System;

namespace BookProject.Application.Models
{
    public class ArticleModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid MagazineId { get; set; }
        public Guid AuthorId { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
