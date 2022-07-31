using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZakaZaka.Models;
using ZakaZaka.Models.Response;

namespace ZakaZaka.Repositories.Contracts
{
    public interface IAuthRepository
    {
        Task<TokenModel> AddTokens(TokenModel model);
        Task DeleteTokens(TokenModel model);
        Task<TokenModel> GetTokenById(Guid id);
        Task<List<RefreshToken>> GetUserRefreshTokensByUserCredentialIdAsync(Guid id);
        Task<RefreshToken> GetRefreshTokenByTokenAsync(string token);
        Task UpdateRefreshTokenAsync(RefreshToken refreshToken);
        Task InsertRefreshTokenAsync(RefreshToken refreshToken);

    }
}
