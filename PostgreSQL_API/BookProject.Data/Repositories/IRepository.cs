using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookProject.Data.Repositories;

namespace BookProject.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        /*IRepository<T> sınıfı, herhangi bir nesne için CRUD işlemlerini sağlamak için genel bir arayüz sağlar.
        Tüm CRUD işlemleri, "T" tipindeki herhangi bir sınıfı işleyebilen yöntemler içerir.
        Örneğin, Post, Comment, User gibi farklı veri modelleri için kullanılabilir.*/
        //GENEL CRUD İŞLEMİ
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(int id);
    }
}
