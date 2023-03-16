using System;

namespace BookProject.Data.Models
{
    public class ArticleJoinModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid MagazineId { get; set; }
        public string MagazineName { get; set; }
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
    }
}
