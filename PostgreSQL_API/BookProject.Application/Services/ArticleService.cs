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
using BookProject.Application.Models;
using BookProject.Data.Models;

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
        public IQueryable<ArticleJoinModel> GetArticleJoinModels()
        {
            return _articleRepository.GetArticleJoinModels();
        }

        public async Task<Article> AddAsync(ArticleModel articleModel)
        {
            var article = new Article
            {
                id = articleModel.id,
                Title= articleModel.Title,
                Content= articleModel.Content,
                AuthorId= articleModel.AuthorId,
                MagazineId= articleModel.MagazineId
            };

            return await _articleRepository.AddAsync(article);
        }

        public async Task<Article> UpdateAsync(ArticleModel articleModel)
        {
            var article = await _articleRepository.GetByIdAsync(articleModel.id);

            if(article == null)
            {
                // Kullanıcı bulunamadı
                return null;
            }
            article.Content = articleModel.Content ?? article.Content;
            article.Title = articleModel.Title ?? article.Title;
            int authorId = articleModel.AuthorId;
            if(articleModel.AuthorId != 0)
            {
                article.AuthorId = articleModel.AuthorId;
            }
            else
            {
                article.AuthorId = article.AuthorId;
            }

            if(articleModel.MagazineId != 0)
            {
                article.MagazineId = articleModel.MagazineId;
            }
            else
            {
                article.MagazineId = article.MagazineId;
            }

            //article.AuthorId = articleModel.AuthorId ?? article.AuthorId;
            //article.MagazineId = articleModel.MagazineId ?? article.MagazineId;



            return await _articleRepository.UpdateAsync(article);
        }
        
        public async Task<Article> DeleteAsync(int id)
        {
            return await _articleRepository.DeleteAsync(id);
        }
    }
}
