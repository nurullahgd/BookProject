using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookProject.Application.Models;
using BookProject.Data.Entities;
using BookProject.Data.Models;

namespace BookProject.Application.Interfaces
{
    public interface IArticleService
    {
        
        Task<Article> GetByIdAsync(int id);
        Task<IEnumerable<Article>> GetAllAsync();
        Task<Article> AddAsync(ArticleModel article);
        Task<Article> UpdateAsync(ArticleModel article);
        Task<Article> DeleteAsync(int id);
        IQueryable<ArticleJoinModel> GetArticleWithUserAndMagazine();

    }
}