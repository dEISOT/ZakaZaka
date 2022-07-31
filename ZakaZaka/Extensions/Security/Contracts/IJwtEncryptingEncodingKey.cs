using Microsoft.IdentityModel.Tokens;

namespace ZakaZaka.Extensions.Security.Contracts
{
    public interface IJwtEncryptingEncodingKey
    {
        string SigningAlgorithm { get; }

        string EncryptingAlgorithm { get; }

        SecurityKey GetKey();
    }
}
