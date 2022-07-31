using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZakaZaka.Models;

namespace ZakaZaka.Services.Contracts
{
    public interface IShopService
    {
        Task<IEnumerable<Shop>> GetAsync();

        Task<IEnumerable<Shop>> SearchAsync(ShopFilter shopFilter);

        Task<Shop> GetShopById(int id);

        Task<int> AddNewShopAsync(Shop entity);

        Task<Shop> UpdateShopAsnyc(int id, Shop entity);

        Task DeleteShopAsync(int id);

    }
}
