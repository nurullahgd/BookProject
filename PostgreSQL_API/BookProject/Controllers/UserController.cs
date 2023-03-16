using Microsoft.AspNetCore.Mvc;
using BookProject.Application.Interfaces;
using System.Threading.Tasks;
using BookProject.Application.Models;
using AutoMapper;
using BookProject.Application.Mapper;
using BookProject.Application.Validation.UserValidation;
using BookProject.Data;
using BookProject.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using System;

namespace BookProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        IMapper mapper = BookProjectMapper.Mapper;
        private readonly IUserService _userService;
        private readonly UserAddValidator _userValidator;
        private readonly UserUpdateValidator _userUpdateValidator;
        

        public UserController(IUserService userservice)
        {
            _userService = userservice;
            _userValidator = new UserAddValidator(_userService);
            _userUpdateValidator = new UserUpdateValidator(_userService);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            
            var user = await _userService.GetByIdAsync(id);
            if(user == null)
            {
                return BadRequest("Invalid User ID");
            }
            var userModel = mapper.Map<UserResponse>(user);
            return Ok(userModel);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var user = await _userService.GetAllAsync();
            return Ok(user);

        }

        [HttpPost]
        public async Task<IActionResult> Create(UserResponse user)
        {
            if(user == null)
            {
                return BadRequest("User information is missing.");
            }

            var validationResult = await _userValidator.ValidateAsync(user);
            if(!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var newUser = await _userService.AddAsync(user);
            //var msgsender = new UserMessageSender();
            //var msgsendermap = mapper.Map<User>(user);
            //msgsender.SendUserAddedMessage(msgsendermap);
            return Ok(newUser);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UserModel user)
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

            var updatedUserModel = new UserModel
            {
                Id=existingUser.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
            var validationResult = await _userUpdateValidator.ValidateAsync(updatedUserModel);
            if(!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var updatedUser = await _userService.UpdateAsync(updatedUserModel);
            //var msgsender = new UserMessageSender();
            //var msgsendermap = mapper.Map<User>(updatedUser);
            //msgsender.SendUserUpdatedMessage(msgsendermap);
            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existingArticle = await _userService.GetByIdAsync(id);
            if(existingArticle == null)
            {
                return BadRequest("Error. Invalid ID!");
            }

            var msgsender = new UserMessageSender();
            msgsender.SendUserDeletedMessage(existingArticle);
            await _userService.DeleteAsync(id);

            return Ok("Deleted");
        }
    }
}
