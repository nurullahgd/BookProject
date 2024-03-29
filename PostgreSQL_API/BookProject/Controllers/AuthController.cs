﻿using BookProject.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        private readonly AccountLoginValidation _accountLoginValidator;
        public AuthController(IConfiguration configuration, IAccountService accountService)
        {
            _configuration = configuration;
            _accountService = accountService;
            _accountValidator = new AccountValidator(_accountService);
            _accountLoginValidator = new AccountLoginValidation(_accountService);
        }
        public static Account acc = new Account();
        private readonly IConfiguration _configuration;

        [HttpPost]
        public async Task<IActionResult> Register(AccountResponse request)
        {

            var validationResult = await _accountValidator.ValidateAsync(request);
            if(!validationResult.IsValid) return BadRequest(validationResult.Errors);

            if(request == null) return BadRequest("User information is missing.");
            var newacc = await _accountService.AddAsync(request);
            return Ok(newacc);
        }


        [HttpPost]
        public async Task<IActionResult> Login(AccountResponse request)
        {
            var validationResult = _accountLoginValidator.Validate(request);
            if(!validationResult.IsValid) return BadRequest(validationResult.Errors);
            var user = await _accountService.FindPassword(request.Username, request.Password);
            if(user == null) return BadRequest("Wrong password");
            acc.Username = request.Username;
            string token = CreateToken(acc);
            return Ok(token);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _accountService.GetAllAsync();
            return Ok(data);
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
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

    }
}
