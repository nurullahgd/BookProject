using BookProject.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using BookProject.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BookProject.Data.Repositories
{
    public class ArticleRepository : BaseRepository<Article>, IArticleRepository
    {
        private readonly PostgreSqlExampleDbContext _context;

        public ArticleRepository(PostgreSqlExampleDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<ArticleJoinModel> GetArticleWithUserAndMagazine()
        {
            var result = _context.Articles
                 .Include(a => a.Magazine)
                 .Include(a => a.Author)
                 .Select(a => new ArticleJoinModel
                 {
                     id = a.id,
                     Title = a.Title,
                     Content = a.Content,
                     MagazineId = a.MagazineId,
                     MagazineName = a.Magazine.Name,
                     AuthorId=a.AuthorId,
                     AuthorName = a.Author.FirstName + " " + a.Author.LastName
                 }); ;
            return result;
        }
    }

    public interface IArticleRepository : IRepository<Article>
    {
        IQueryable<ArticleJoinModel> GetArticleWithUserAndMagazine();
    }
}
