using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZakaZaka.Data;
using ZakaZaka.Models;
using ZakaZaka.Repositories.Contracts;

namespace ZakaZaka.Repositories
{
    public class ShopRepository : IShopRepository
    {
        private readonly ZakaDbContext _db;
        private readonly IMapper _mapper;

        public ShopRepository(ZakaDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Shop>> GetAsync()
        {
            return await _db.Shops.ToListAsync();
        }

        public async Task<IEnumerable<Shop>> SearchAsync(ShopFilter shopFilter)
        {
            var result = await _db.Shops.Where(s => string.Equals(shopFilter.Name, s.Name)).ToListAsync();

            return result;
        }

        public async Task<int> AddNewShopAsync(Shop entity)
        {
            await _db.Shops.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<Shop> UpdateShopAsync(int id, Shop entity)
        {
            var result = await _db.Shops.FirstOrDefaultAsync(s => s.Id == id);
            result.Name = entity.Name;
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<Shop> GetShopById(int id)
        {
            var result = await _db.Shops.FirstOrDefaultAsync(s => s.Id == id);
            return result;
        }

        public async Task DeleteShopAsync(Shop shop)
        {
            _db.Shops.Remove(shop);
            await _db.SaveChangesAsync();
        }
    }
}
