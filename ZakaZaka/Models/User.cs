using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using ZakaZaka.Models.Request;

namespace ZakaZaka.Models
{
    public class User
    {
        [Key, ForeignKey("UserCredential")]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [JsonIgnore]
        public virtual UserCredential Credential { get; set; }

    }
}
