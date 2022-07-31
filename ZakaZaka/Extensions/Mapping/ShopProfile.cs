using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZakaZaka.Models;
using ZakaZaka.Models.Request;

namespace ZakaZaka.Extensions
{
    public class ShopProfile : Profile
    {
        public ShopProfile()
        {
            CreateMap<ShopRequestModel, Shop>();
            CreateMap<UpdateShopRequestModel, Shop>();
        }
    }
}
