using System;

namespace ZakaZaka.Models
{
    public class RefreshToken
    {
        public Guid RefreshTokenId { get; set; }
        public string Token { get; set; }
        public Guid UserCredentialsId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expires { get; set; }
        public DateTime Revoked { get; set; }
    }
}
