using Microsoft.AspNetCore.Mvc;
using BookProject.Application.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using BookProject.Application.Models;
using AutoMapper;
using BookProject.Application.Mapper;
using BookProject.Application.Validation.ArticleValidation;
using System;
using BookProject.Data.Messages;
using BookProject.Data.Entities;
using Newtonsoft.Json;

namespace BookProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class ArticleController : ControllerBase
    {
        IMapper mapper = BookProjectMapper.Mapper;
        private readonly IArticleService _articleService;
        private readonly ArticleAddValidator _articleaddValidator;
        private readonly IUserService _userService;
        private readonly IMagazineService _magazineService;
        
        public ArticleController(IUserService userservice, IArticleService articleService,IMagazineService magazineservice)
        {
            _magazineService = magazineservice;
            _userService = userservice;
            _articleService = articleService;
            _articleaddValidator = new ArticleAddValidator(_userService,_articleService,_magazineService);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            
            var article = await _articleService.GetByIdAsync(id);
            
            if(article == null)
            {
                return BadRequest("Invalid ID");
            }
            var articleModel = mapper.Map<ArticleModel>(article); 
            return Ok(articleModel);
        }

        [HttpGet]
        public ActionResult<List<ArticleResponse>> GetArticleWithUserAndMagazineJustName()
        {
            var articles = _articleService.GetArticleWithUserAndMagazine();
            var response = mapper.Map<List<ArticleResponseNames>>(articles);
            return Ok(response);
        }

        [HttpGet]
        
        public async Task<IActionResult> GetAll()
        {
            var articles = await _articleService.GetAllAsync();
            //var response = mapper.Map<ArticleModel>(articles);
            return Ok(articles);

        }

        [HttpPost]
        public async Task<IActionResult> Create(ArticleModel article)
        {
            if(article==null)
            {
                return BadRequest("Article information is missing");
            }
            var validationResult = await _articleaddValidator.ValidateAsync(article);
            if(!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var _msgsender = new ArticleMessageSender();
            var newArticle = await _articleService.AddAsync(article);
            var articleResponse = mapper.Map<ArticleResponse>(newArticle);
            var msgsendermap = mapper.Map<Article>(newArticle);
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var message = JsonConvert.SerializeObject(msgsendermap, settings);
            //var test = mapper.Map<Article>(message);
            _msgsender.SendArticleAddedMessage(message);
            return Ok(articleResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, ArticleModel article)
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
                Id = existingArticle.Id,
                Title = article.Title,
                Content=article.Content,
                AuthorId=article.AuthorId,
                MagazineId=article.MagazineId
            };
            
            var updatedArticle = await _articleService.UpdateAsync(updatedArticleModel);
            var articleResponse = mapper.Map<ArticleModel>(updatedArticle);
            return Ok(articleResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
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
