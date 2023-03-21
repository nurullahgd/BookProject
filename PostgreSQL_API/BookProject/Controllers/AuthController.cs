using BookProject.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using BookProject.Application.Interfaces;
using BookProject.Application.Mapper;
using AutoMapper;
using BookProject.Application.Models;
using BookProject.Application.Validation.AccountValidation;

namespace BookProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IMapper mapper = BookProjectMapper.Mapper;
        private readonly IAccountService _accountService;
        private readonly AccountValidator _accountValidator;
        public AuthController(IConfiguration configuration, IAccountService accountService)
        {
            _configuration = configuration;
            _accountService = accountService;
            _accountValidator = new AccountValidator(_accountService);
        }
        public static AccountHashes acc = new AccountHashes();
        private readonly IConfiguration _configuration;

        [HttpPost("register")]
        public async Task<ActionResult<AccountHashes>> Register(AccountResponse request)
        {
            var validationResult = await _accountValidator.ValidateAsync(request);
            if(!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            if(request == null)
            {
                return BadRequest("User information is missing.");
            }
            var newacc = await _accountService.AddAsync(request);
            return Ok(newacc);
        }


        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(AccountResponse request)
        {
            var user = await _accountService.FindUsernameAndPassword(request.Username,request.Password);
            //var test = mapper.Map<AccountResponse>(user);
            if(user == null)
            {
                return BadRequest("Wrong User or password");
            }
            CreatePasswordHash(request.Password, out byte[] Passwordhash, out byte[] PasswordSalt);
            acc.Username = request.Username;
            acc.PasswordHash = Passwordhash;
            acc.PasswordSalt = PasswordSalt;
            string token = CreateToken(acc);
            return Ok(token);
        }
        [HttpGet("ALL DATA")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _accountService.GetAllAsync();
            return Ok(data);
        }

        private string CreateToken(AccountHashes acc)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, acc.Username)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        private void CreatePasswordHash(string password, out byte[] Passwordhash, out byte[] PasswordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                Passwordhash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] Passwordhash, byte[] PasswordSalt)
        {
            using(var hmac = new HMACSHA512(PasswordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(Passwordhash);
            }
        }

    }
}
