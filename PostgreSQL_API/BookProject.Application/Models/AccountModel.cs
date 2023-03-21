using System;

namespace BookProject.Application.Models
{
    public class AccountModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
