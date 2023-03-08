using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookProject.Application.Interfaces;
using BookProject.Data.Repositories;
using BookProject.Application.Services;
using BookProject.Data;
using BookProject.Data.Entities;

namespace BookProject.Application.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleService(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<Article> GetByIdAsync(int id)
        {
            return await _articleRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Article>> GetAllAsync()
        {
            return await _articleRepository.GetAllAsync();
        }

        public async Task<Article> AddAsync(Article article)
        {
            return await _articleRepository.AddAsync(article);
        }

        public async Task<Article> UpdateAsync(Article article)
        {
            return await _articleRepository.UpdateAsync(article);
        }

        public async Task<Article> DeleteAsync(int id)
        {
            return await _articleRepository.DeleteAsync(id);
        }
    }
}
