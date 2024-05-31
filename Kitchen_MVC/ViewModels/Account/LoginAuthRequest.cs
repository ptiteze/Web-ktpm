﻿using System.ComponentModel.DataAnnotations;

namespace Kitchen_MVC.ViewModels.Account
{
    public class LoginAuthRequest
    {
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
