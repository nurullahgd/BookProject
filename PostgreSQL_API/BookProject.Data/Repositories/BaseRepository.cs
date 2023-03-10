using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookProject.Data.Entities;
using Microsoft.EntityFrameworkCore;
using BookProject.Data.Models;

namespace BookProject.Data.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        public readonly DbContext _context;

        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public IQueryable<ArticleJoinModel> GetArticleWithUserAndMagazine()
        {
            var articles = _context.Set<Article>()
        .Include(a => a.Magazine)
        .Include(a => a.Author)
        .Select(a => new ArticleJoinModel
        {
            id = a.id,
            Title = a.Title,
            Content = a.Content,
            MagazineId = a.MagazineId,
            MagazineName = a.Magazine.Name,
            AuthorId = a.AuthorId,
            AuthorName = a.Author.FirstName + " " + a.Author.LastName
        });

            return articles;
        }



        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        
        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
        
        public async Task<T> DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
