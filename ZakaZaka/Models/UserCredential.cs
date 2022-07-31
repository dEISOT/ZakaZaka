using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZakaZaka.Models
{
    public class UserCredential
    {
        [Key]
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
