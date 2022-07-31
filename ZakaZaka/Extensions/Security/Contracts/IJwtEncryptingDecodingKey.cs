using Microsoft.IdentityModel.Tokens;

namespace ZakaZaka.Extensions.Security.Contracts
{
    public interface IJwtEncryptingDecodingKey
    {
        SecurityKey GetKey();

    }
}
