using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZakaZaka.Data;
using ZakaZaka.Models;
using ZakaZaka.Models.Response;
using ZakaZaka.Repositories.Contracts;

namespace ZakaZaka.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ZakaDbContext _db;

        public AuthRepository(ZakaDbContext db)
        {
            _db = db;
        }


        public async Task<TokenModel> AddTokens(TokenModel model)
        {
            await _db.TokenModels.AddAsync(model);
            await _db.SaveChangesAsync();
            return model;
        }

        public async Task DeleteTokens(TokenModel model)
        {
            _db.TokenModels.Remove(model);
            await _db.SaveChangesAsync();
        }

        public async Task<TokenModel> GetTokenById(Guid id)
        {
            var result = await _db.TokenModels.FirstOrDefaultAsync(t => t.UserId == id);
            return result;
        }
        public async Task<RefreshToken> GetRefreshTokenByTokenAsync(string token)
        {
            return await _db.RefreshTokens
               .FirstOrDefaultAsync(r =>
                   r.Token.Equals(token));
        }

        public async Task<List<RefreshToken>> GetUserRefreshTokensByUserCredentialIdAsync(Guid UserCredentialsId)
        {
            return await _db.RefreshTokens
                .Where(r =>
                  r.UserCredentialsId == UserCredentialsId)
                .ToListAsync();
        }

        public async Task UpdateRefreshTokenAsync(RefreshToken refreshToken)
        {
            _db.RefreshTokens.Update(refreshToken);
            await _db.SaveChangesAsync();
        }

        public async Task InsertRefreshTokenAsync(RefreshToken refreshToken)
        {
            _db.RefreshTokens.Add(refreshToken);
            await _db.SaveChangesAsync();
        }
    }
}
