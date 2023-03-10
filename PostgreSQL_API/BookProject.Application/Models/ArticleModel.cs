namespace BookProject.Application.Models
{
    public class ArticleModel
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int MagazineId { get; set; }
        public int AuthorId { get; set; }
    }
}
