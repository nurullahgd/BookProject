using Microsoft.AspNetCore.Mvc;
using BookProject.Application.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using BookProject.Application.Models;
using AutoMapper;
using BookProject.Application.Mapper;

using BookProject.Application.Validation.ArticleValidation;
using System;
using Microsoft.AspNetCore.Authorization;

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
        private readonly ArticleUpdateValidator _articleUpdateValidator;
        private readonly IUserService _userService;
        private readonly IMagazineService _magazineService;
        public ArticleController(IUserService userservice, IArticleService articleService,IMagazineService magazineservice)
        {
            _magazineService = magazineservice;
            _userService = userservice;
            _articleService = articleService;
            _articleaddValidator = new ArticleAddValidator(_userService,_articleService,_magazineService);
            _articleUpdateValidator = new ArticleUpdateValidator(_userService, _articleService, _magazineService);

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
        public async Task<ActionResult<List<ArticleResponse>>> GetArticleWithUserAndMagazineJustName()
        {
            var articles = _articleService.GetArticleWithUserAndMagazine();
            var response = mapper.Map<List<ArticleResonseNames>>(articles);
            return Ok(response);
        }

        [HttpGet]
        
        public async Task<IActionResult> GetAll()
        {
            var articles =  _articleService.GetArticleWithUserAndMagazine();
            var response = mapper.Map<List<ArticleResponse>>(articles);
            return Ok(response);

        }

        [HttpPost]
        public async Task<IActionResult> Create(ArticleResponse article)
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
            
            var newArticle = await _articleService.AddAsync(article);
            var articleResponse = mapper.Map<ArticleResponse>(newArticle);

            return Ok(newArticle);
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
            var validationResult = await _articleUpdateValidator.ValidateAsync(article);
            if(!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }


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
