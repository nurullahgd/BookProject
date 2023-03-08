using Microsoft.AspNetCore.Mvc;
using BookProject.Data.Entities;
using BookProject.Application.Interfaces;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

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
            return Ok(article);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var articles = await _articleService.GetAllAsync();
            return Ok(articles);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Article article)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newArticle = await _articleService.AddAsync(article);

            return Ok(newArticle);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Article article)
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

            existingArticle.Title = article.Title;
            //existingArticle.Description = article.Description;

            var updatedArticle = await _articleService.UpdateAsync(existingArticle);

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
