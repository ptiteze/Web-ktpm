using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Kitchen_MVC.DTO.Account
{
    public class VerifyOTPRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string OTP { get; set; }

        [JsonIgnore]
        public string? Type { get; set; }
    }
}
