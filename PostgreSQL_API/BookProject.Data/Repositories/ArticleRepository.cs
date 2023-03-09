using BookProject.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using BookProject.Data.Models;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace BookProject.Data.Repositories
{
    public class ArticleRepository : BaseRepository<Article>, IArticleRepository
    {
        private readonly PostgreSqlExampleDbContext _context;

        public ArticleRepository(PostgreSqlExampleDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<ArticleJoinModel> GetArticleJoinModels()
        {
            var result = from article in _context.Articles
                         join magazine in _context.Magazines on article.MagazineId equals magazine.Id
                         join user in _context.Users on article.AuthorId equals user.Id
                         select new ArticleJoinModel
                         {
                             id = article.id,
                             Title = article.Title,
                             Content = article.Content,
                             MagazineName = magazine.Name,
                             AuthorName = user.FirstName + " " + user.LastName
                         };
            return result;
        }
    }

    public interface IArticleRepository : IRepository<Article>
    {
        IQueryable<ArticleJoinModel> GetArticleJoinModels();
    }
}
