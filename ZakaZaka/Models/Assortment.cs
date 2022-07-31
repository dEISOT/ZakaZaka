using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZakaZaka.Models
{
    public class Assortment
    {
        public int Id { get; set; }
        public int ShopId { get; set; }
        public virtual Shop Shop { get; set; }
        public int GameId { get; set; }
        public virtual Game Game { get; set; }

    }
}
