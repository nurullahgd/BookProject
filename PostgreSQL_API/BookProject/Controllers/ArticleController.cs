using Microsoft.AspNetCore.Mvc;
using BookProject.Data.Entities;
using BookProject.Application.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using BookProject.Application.Models;
using BookProject.Data.Models;
using AutoMapper;
using BookProject.Application.Mapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;
        IMapper mapper = BookProjectMapper.Mapper;
        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if(id <= 0) 
            {
                return BadRequest("Invalid article ID");
            }
            var article = await _articleService.GetByIdAsync(id);
            
            if(article == null)
            {
                return NotFound();
            }
            var articleModel = mapper.Map<ArticleResponse>(article); 
            return Ok(articleModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var articles =  _articleService.GetArticleJoinModels();
            return Ok(articles);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ArticleModel article)
        {
            if(article==null)
            {
                return BadRequest("Article information is missing");
            }
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newArticle = await _articleService.AddAsync(article);

            return Ok(newArticle);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ArticleModel article)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var existingArticle = await _articleService.GetByIdAsync(id);
            if(existingArticle == null)
            {
                return NotFound();
            }
            var updatedArticleModel = new ArticleModel
            {
                id = existingArticle.id,
                Title = article.Title,
                Content=article.Content,
                AuthorId=article.AuthorId,
                MagazineId=article.MagazineId
            };

            var updatedArticle = await _articleService.UpdateAsync(updatedArticleModel);

            return Ok(updatedArticle);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingArticle = await _articleService.GetByIdAsync(id);
            if(existingArticle == null)
            {
                return BadRequest("Error. Invalid ID!");
            }

            await _articleService.DeleteAsync(id);

            return Ok("Deleted");
        }
       
    }
}
