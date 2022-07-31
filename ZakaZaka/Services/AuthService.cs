using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ZakaZaka.Models;
using ZakaZaka.Models.Request;
using ZakaZaka.Models.Response;
using ZakaZaka.Repositories.Contracts;
using ZakaZaka.Services.Contracts;

namespace ZakaZaka.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthService(IAuthRepository authRepository, IMapper mapper, IConfiguration configuration, IUserService userService, IUserRepository userRepository)
        {
            _authRepository = authRepository;
            _mapper = mapper;
            _configuration = configuration;
            _userService = userService;
            _userRepository = userRepository;
        }

        public async Task<AuthResponse> AuthenticateAsync(UserLoginRequest model)
        {
            try
            {
                var user = await _userService.FindByEmailAsync(model.Email);
                if (user is null)
                {

                }
                var check = BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash);
                if(!check)
                {
                    //exception
                }

                // User exists and password is correct so JWT and Refresh Token can be generated
                var jwtToken = GenerateJwtToken(user.Id);
                var refreshToken = GenerateRefreshToken();

                refreshToken.UserCredentialsId = user.Credential.Id;

                var oldUserRefreshTokenList = await GetUserRefreshTokensAsync(user.Id);

                if(oldUserRefreshTokenList != null)
                { 
                    // Search for still active tokens in the list and revoke them if needed
                    var activeTokens = oldUserRefreshTokenList.Where(r => r.Revoked == null);
                    foreach (var activeToken in activeTokens)
                    {
                        if (activeToken.Revoked == null)
                            await RevokeTokenAsync(activeToken.Token);
                    }
                }

                await _authRepository.InsertRefreshTokenAsync(refreshToken);

                var tokenResponse = new TokenResponse(jwtToken, refreshToken.Token);

                var authenticateResponse =
                    new AuthResponse(tokenResponse.Token, tokenResponse.RefreshToken);

                return authenticateResponse;

            }
                catch (Exception)
            {

                throw;
            }
           
        }
        public RefreshToken GenerateRefreshToken()
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);

                // Get expiration days from configuration
                var RefreshTokenExpiration = double
                    .Parse(_configuration.GetValue<string>("JWT:RefreshTokenExpiresDays"));
                return new RefreshToken()
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.UtcNow.AddDays(RefreshTokenExpiration),
                    Created = DateTime.UtcNow
                };
            }
        }
        public string GenerateJwtToken(Guid id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // Get secret phrase from configuration
            var secret = _configuration.GetValue<string>("JWT:Secret");

            // Get expiration minutes from configuration
            var JwtTokenExpiration = double.Parse(_configuration.GetValue<string>("JWT:JwtTokenExpiresMinutes"));

            // Encode secrets
            var key = Encoding.UTF8.GetBytes(secret);
            

            // Set JWT description using user id and user role
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, id.ToString()),
                }),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
                
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(JwtTokenExpiration),
            };

            // Generate JWT
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Return serialized JWT in compact format
            return tokenHandler.WriteToken(token);
        }
        public async Task<AuthResponse> GetTokenById(Guid id)
        {
            var tokens = await _authRepository.GetTokenById(id);
            var result = _mapper.Map<AuthResponse>(tokens);
            return result;
        }
        public async Task<bool> RevokeTokenAsync(string token)
        {
            try
            {
                var refreshToken = await _authRepository.GetRefreshTokenByTokenAsync(token);
                if(refreshToken == null)
                {
                    //message + exception
                }
                refreshToken.Revoked = DateTime.UtcNow;
                //ip
                await _authRepository.UpdateRefreshTokenAsync(refreshToken);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<RefreshToken>> GetUserRefreshTokensAsync(Guid id)
        {
            try
            {
                var userCredentials = await _userRepository.GetCredentialByUserIdAsync(id);
                var refreshTokenList = await _authRepository.GetUserRefreshTokensByUserCredentialIdAsync(userCredentials.Id);
                if(refreshTokenList == null)
                {
                    //message + exception
                }
                return refreshTokenList;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
