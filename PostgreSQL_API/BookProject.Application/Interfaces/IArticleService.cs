using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookProject.Data.Entities;

namespace BookProject.Application.Interfaces
{
    public interface IArticleService
    {
        /*IArticleService, sadece Article sınıfı için CRUD işlemlerini sağlar.
         Bu nedenle, uygulamanın sadece Article nesneleriyle ilgilendiği bir senaryoda kullanılır.
         IRepository<T> kullanımı ise uygulamanın herhangi bir sınıfı için CRUD işlemleri yapılması gereken durumlarda kullanılır.*/

        Task<Article> GetByIdAsync(int id);
        Task<IEnumerable<Article>> GetAllAsync();
        Task<Article> AddAsync(Article article);
        Task<Article> UpdateAsync(Article article);
        Task<Article> DeleteAsync(int id);
    }
}
