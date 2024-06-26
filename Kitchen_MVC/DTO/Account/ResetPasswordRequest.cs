﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Kitchen_MVC.DTO.Account
{
    public class ResetPasswordRequest
    {
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? OTP { get; set; }
        [Required]
        [JsonIgnore]
        public string? Type { get; set; }
    }
}
