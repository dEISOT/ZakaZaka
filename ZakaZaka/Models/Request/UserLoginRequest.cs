using System.ComponentModel.DataAnnotations;

namespace ZakaZaka.Models.Request
{
    public class UserLoginRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
