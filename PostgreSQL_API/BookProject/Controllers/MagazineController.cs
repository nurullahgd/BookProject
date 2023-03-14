using Microsoft.AspNetCore.Mvc;
using BookProject.Application.Interfaces;
using System.Threading.Tasks;
using BookProject.Application.Models;
using BookProject.Application.Mapper;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using BookProject.Application.Validation.MagazineValidation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class MagazineController : ControllerBase
    {
        private readonly IMagazineService _magazineService;
        IMapper mapper = BookProjectMapper.Mapper;
        private readonly MagazineAddValidator _magazineValidator;
        private readonly MagazineUpdateValidator _magazineUpdateValidator;
        public MagazineController(IMagazineService magazineservice)
        {
            _magazineService = magazineservice;
            _magazineValidator = new MagazineAddValidator(_magazineService);
            _magazineUpdateValidator = new MagazineUpdateValidator(_magazineService);
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
            var magazineModel = mapper.Map<MagazineResponse>(magazine);
            return Ok(magazineModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var magazine = await _magazineService.GetAllAsync();
            return Ok(magazine);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MagazineModel magazine)
        {
            if(magazine == null)
            {
                return BadRequest("Magazine information is missing");
            }
            if(!ModelState.IsValid)
            {
                return BadRequest("Please fill in the fields correctly.");
            }
            var validationResult = await _magazineValidator.ValidateAsync(magazine);
            if(!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var newMagazine = await _magazineService.AddAsync(magazine);

            return Ok(newMagazine);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MagazineModel magazine)
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

            var updatedMagazineModel = new MagazineModel
            {
                Id = existingMagazine.Id,
                Name = magazine.Name
            };
            var validationResult = await _magazineUpdateValidator.ValidateAsync(magazine);
            if(!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var updatedMagazine = await _magazineService.UpdateAsync(updatedMagazineModel);

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
