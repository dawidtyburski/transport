﻿using System.ComponentModel.DataAnnotations;

namespace transport.Models
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
