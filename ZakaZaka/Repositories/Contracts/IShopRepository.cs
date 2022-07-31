using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZakaZaka.Models;

namespace ZakaZaka.Repositories.Contracts
{
    public interface IShopRepository
    {
        Task<IEnumerable<Shop>> GetAsync();

        Task<IEnumerable<Shop>> SearchAsync(ShopFilter shopFilter);

        Task<Shop> GetShopById(int id);

        Task<int> AddNewShopAsync(Shop entity);

        Task<Shop> UpdateShopAsync(int id, Shop entity);

        Task DeleteShopAsync(Shop shop);

    }
}
