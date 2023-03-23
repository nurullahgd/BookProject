using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookProject.Application.Interfaces;
using BookProject.Data.Repositories;
using BookProject.Data.Entities;
using BookProject.Application.Models;
using BookProject.Data.Models;
using AutoMapper;

namespace BookProject.Application.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;


        public ArticleService(IArticleRepository articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }

        public IQueryable<ArticleJoinModel> GetArticleWithUserAndMagazine()
        {
                return _articleRepository.GetArticleWithUserAndMagazine();
            
        }

        public async Task<Article> GetByIdAsync(Guid id)
        {
            return await _articleRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Article>> GetAllAsync()
        {
            return await _articleRepository.GetAllAsync();
        }
        

        public async Task<Article> AddAsync(ArticleModel articleModel)
        {
            var article = new Article
            {
                Title = articleModel.Title,
                Content = articleModel.Content,
                AuthorId = articleModel.AuthorId,
                MagazineId = articleModel.MagazineId,
                CreatedTime = DateTime.Now
            };

            return await _articleRepository.AddAsync(article);
        }

        public async Task<Article> UpdateAsync(ArticleModel articleModel)
        {
            var article = await _articleRepository.GetByIdAsync(articleModel.Id);

            if(article == null)
            {
                // Kullanıcı bulunamadı
                return null;
            }
            article.Content = articleModel.Content ?? article.Content;
            article.Title = articleModel.Title ?? article.Title;
            if(articleModel.MagazineId==null)
            {
                article.MagazineId = article.MagazineId;
            }
            else
            {
                article.MagazineId = articleModel.MagazineId;
            }
            if(articleModel.AuthorId==null)
            {
                article.AuthorId = article.AuthorId;
            }
            else
            {
                article.AuthorId = articleModel.AuthorId;
            }
             


            //article.MagazineId = articleModel.MagazineId ?? article.MagazineId;


            return await _articleRepository.UpdateAsync(article);
        }

        public async Task<Article> DeleteAsync(Guid id)
        {
            return await _articleRepository.DeleteAsync(id);
        }
    }
}