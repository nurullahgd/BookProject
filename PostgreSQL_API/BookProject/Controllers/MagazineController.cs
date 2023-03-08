using Microsoft.AspNetCore.Mvc;
using BookProject.Data.Entities;
using BookProject.Application.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MagazineController : ControllerBase
    {
        private readonly IMagazineService _magazineService;

        public MagazineController(IMagazineService magazineservice)
        {
            _magazineService = magazineservice;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if(id <= 0)
            {
                return BadRequest("Invalid Magazine ID");
            }

            var magazine = await _magazineService.GetByIdAsync(id);
            if(magazine == null)
            {
                return NotFound();
            }
            return Ok(magazine);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var magazine = await _magazineService.GetAllAsync();
            return Ok(magazine);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Magazine magazine)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Please fill in the fields correctly.");
            }

            var newMagazine = await _magazineService.AddAsync(magazine);

            return Ok(newMagazine);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Magazine magazine)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var existingMagazine = await _magazineService.GetByIdAsync(id);
            if(existingMagazine == null)
            {
                return NotFound();
            }
            existingMagazine.Name=magazine.Name;
            existingMagazine.Articles = magazine.Articles;

            var updatedMagazine = await _magazineService.UpdateAsync(existingMagazine);

            return Ok(updatedMagazine);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingArticle = await _magazineService.GetByIdAsync(id);
            if(existingArticle == null)
            {
                return BadRequest("Error. Invalid ID!");
            }

            await _magazineService.DeleteAsync(id);

            return Ok("Deleted");
        }
    }
}
