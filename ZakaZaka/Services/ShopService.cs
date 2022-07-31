using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ZakaZaka.Models;
using ZakaZaka.Repositories.Contracts;
using ZakaZaka.Services.Contracts;

namespace ZakaZaka.Services
{
    public class ShopService : IShopService
    {
        private readonly IShopRepository _shopRepository;

        public ShopService(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }

        public async Task<IEnumerable<Shop>> GetAsync()
        {
            var shops = await _shopRepository.GetAsync();
            return shops;
        }

        public async Task<IEnumerable<Shop>> SearchAsync(ShopFilter shopFilter)
        {
            var shops = await _shopRepository.SearchAsync(shopFilter);
         
            return shops;
        }

        public async Task<Shop> GetShopById(int id)
        {
            var shop = await _shopRepository.GetShopById(id);
            return shop;
        }

        public async Task<int> AddNewShopAsync(Shop entity)
        {
            var shopId = await _shopRepository.AddNewShopAsync(entity);
            return shopId;
        }

        public async Task<Shop> UpdateShopAsnyc(int id, Shop entity)
        {

            var shop = await _shopRepository.UpdateShopAsync(id, entity);
            return shop;
        }

        public async Task DeleteShopAsync(int id)
        {
            try
            {
                var shop = await _shopRepository.GetShopById(id);
                if(shop is null)
                {

                }

                await _shopRepository.DeleteShopAsync(shop);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
