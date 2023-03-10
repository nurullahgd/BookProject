using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookProject.Application.Models;
using BookProject.Data.Entities;
using BookProject.Data.Models;

namespace BookProject.Application.Interfaces
{
    public interface IArticleService
    {
        /*IArticleService, sadece Article sınıfı için CRUD işlemlerini sağlar.
         Bu nedenle, uygulamanın sadece Article nesneleriyle ilgilendiği bir senaryoda kullanılır.
         IRepository<T> kullanımı ise uygulamanın herhangi bir sınıfı için CRUD işlemleri yapılması gereken durumlarda kullanılır.*/
        Task<Article> GetByIdAsync(int id);
        Task<IEnumerable<Article>> GetAllAsync();
        Task<Article> AddAsync(ArticleModel article);
        Task<Article> UpdateAsync(ArticleModel article);
        Task<Article> DeleteAsync(int id);
        IQueryable<ArticleJoinModel> GetArticleWithUserAndMagazine();

    }
}