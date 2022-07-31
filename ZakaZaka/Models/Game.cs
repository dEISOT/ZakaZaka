using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZakaZaka.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Discription { get; set; }

        [MaxLength(64)]
        [MinLength(64)]
        public string PhotoBase64 { get; set; }

        public virtual ICollection<Assortment> Assortments { get; set; }

    }
}
