using BookProject.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace BookProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public static Account acc = new Account();
        private readonly IConfiguration _configuration;

        [HttpPost("register")]
        public async Task<ActionResult<Account>> Register(AccountDto request)
        {
            CreatePasswordHash(request.Password, out byte[] Passwordhash, out byte[] PasswordSalt);

            acc.Username = request.Username;
            acc.PasswordHash = Passwordhash;
            acc.PasswordSalt = PasswordSalt;
            return Ok(acc);
        }
        
        
        [HttpPost("login")]
        public async Task<ActionResult<string>>Login(AccountDto request)
        {
            if(acc.Username!=request.Username)
            {
                return BadRequest("User Not found");
            }
            if(!VerifyPasswordHash(request.Password,acc.PasswordHash,acc.PasswordSalt))
            {
                return BadRequest("Wrong Password");
            }
            string token = CreateToken(acc);
            return Ok(token);
        }

        private string CreateToken(Account acc)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, acc.Username)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims:claims,
                expires:DateTime.Now.AddDays(1),
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

        private bool VerifyPasswordHash(string password,  byte[] Passwordhash,  byte[] PasswordSalt)
        {
            using(var hmac = new HMACSHA512(PasswordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(Passwordhash);
            }
        }

    }
}
