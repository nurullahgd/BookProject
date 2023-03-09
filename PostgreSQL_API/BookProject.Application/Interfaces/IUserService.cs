using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookProject.Application.Models;
using BookProject.Data.Entities;

namespace BookProject.Application.Interfaces
{
    public interface IUserService
    {
        /*IArticleService, sadece User sınıfı için CRUD işlemlerini sağlar.
         Bu nedenle, uygulamanın sadece Article nesneleriyle ilgilendiği bir senaryoda kullanılır.
         IRepository<T> kullanımı ise uygulamanın herhangi bir sınıfı için CRUD işlemleri yapılması gereken durumlarda kullanılır.*/

        Task<User> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> AddAsync(UserModel user);
        Task<User> UpdateAsync(UserModel user);
        Task<User> DeleteAsync(int id);

    }
}
