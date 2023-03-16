using System;

namespace BookProject.Data.Entities
{
    public class Article
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid MagazineId { get; set; }
        public Magazine Magazine { get; set; }
        public Guid AuthorId { get; set; }
        public User Author { get; set; }
    }
}
