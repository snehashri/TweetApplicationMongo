﻿
using System.ComponentModel.DataAnnotations;
namespace TweetApp.Service.ModelDto
{
    public class LoginModelDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
