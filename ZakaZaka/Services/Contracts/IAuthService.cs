using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZakaZaka.Models;
using ZakaZaka.Models.Request;
using ZakaZaka.Models.Response;

namespace ZakaZaka.Services.Contracts
{
    public interface IAuthService
    {
        Task<AuthResponse> AuthenticateAsync(UserLoginRequest model);
        Task<AuthResponse> GetTokenById(Guid id);
        string GenerateJwtToken(Guid id);
        RefreshToken GenerateRefreshToken();
        Task<List<RefreshToken>> GetUserRefreshTokensAsync(Guid id);
    }
}
