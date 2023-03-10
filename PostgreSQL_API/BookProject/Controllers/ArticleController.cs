using Microsoft.AspNetCore.Mvc;
using BookProject.Application.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using BookProject.Application.Models;
using AutoMapper;
using BookProject.Application.Mapper;
using BookProject.Data;
using BookProject.Application.Validation;



namespace BookProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        IMapper mapper = BookProjectMapper.Mapper;
        private readonly IArticleService _articleService;
        private readonly ArticleValidator _articleValidator;
        private readonly IUserService _userService;
        private readonly IMagazineService _magazineService;
        public ArticleController(IUserService userservice, IArticleService articleService,IMagazineService magazineservice)
        {
            _magazineService = magazineservice;
            _userService = userservice;
            _articleService = articleService;
            _articleValidator = new ArticleValidator(_userService,_articleService,_magazineService);
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
            var articleModel = mapper.Map<ArticleModel>(article); 
            return Ok(articleModel);
        }
        //[HttpGet]
        //public async Task<ActionResult<List<ArticleResponse>>> GetArticleWithUserAndMagazine()
        //{
        //    var articles =  _articleService.GetArticleWithUserAndMagazine();
        //    var response = mapper.Map<List<ArticleResponse>>(articles);
        //    return Ok(response);
        //}
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
        public async Task<IActionResult> Create(ArticleModel article)
        {
            if(article==null)
            {
                return BadRequest("Article information is missing");
            }
            var validationResult = await _articleValidator.ValidateAsync(article);
            if(!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
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
