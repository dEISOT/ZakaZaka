using System;
using System.ComponentModel.DataAnnotations;

namespace ZakaZaka.Models
{
    public class TokenModel
    {
        [Key]
        public Guid TokenModelId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public Guid UserId { get; set; }

    }
}
