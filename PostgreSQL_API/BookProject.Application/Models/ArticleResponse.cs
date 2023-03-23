using System;

namespace BookProject.Application.Models
{
    public class ArticleResponse
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid MagazineId { get; set; }
        public string MagazineName { get; set; }
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
