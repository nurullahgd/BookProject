using Microsoft.AspNetCore.Mvc;
using BookProject.Data.Entities;
using BookProject.Application.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using BookProject.Application.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userservice)
        {
            _userService = userservice;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if(id <= 0)
            {
                return BadRequest("Invalid User ID");
            }
            var user = await _userService.GetByIdAsync(id);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpPost]
        //public async Task<IActionResult> Create([FromBody] UserModel user)
        //{
        //    if(user == null)
        //    {
        //        return BadRequest("User information is missing.");
        //    }

        //    if(!ModelState.IsValid)
        //    {
        //        return BadRequest("Please fill in the fields correctly.");
        //    }

        //    var newUser = await _userService.AddAsync(user);

        //    return Ok(newUser);
        //}
        public async Task<IActionResult> Create([FromBody] User  user)
        {
            if(!ModelState.IsValid)
            {
                return NotFound();
            }

            var newUser = await _userService.AddAsync(user);

            //return CreatedAtAction(nameof(Get), new { id = newUser.Id}, newUser);
            return Ok(newUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] User user)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Test Error");
            }

            var existingUser = await _userService.GetByIdAsync(id);
            if(existingUser == null)
            {
                return NotFound();
            }

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;

            var updatedUser = await _userService.UpdateAsync(existingUser);

            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingArticle = await _userService.GetByIdAsync(id);
            if(existingArticle == null)
            {
                return BadRequest("Error. Invalid ID!");
            }


            await _userService.DeleteAsync(id);

            return Ok("Deleted");
        }
    }
}
