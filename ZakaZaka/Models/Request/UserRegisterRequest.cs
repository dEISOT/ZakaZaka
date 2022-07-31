using System.ComponentModel.DataAnnotations;

namespace ZakaZaka.Models.Request
{
    public class UserRegisterRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
