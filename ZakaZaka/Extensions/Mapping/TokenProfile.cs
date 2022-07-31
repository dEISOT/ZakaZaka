using AutoMapper;
using ZakaZaka.Models;
using ZakaZaka.Models.Response;

namespace ZakaZaka.Extensions.Mapping
{
    public class TokenProfile : Profile
    {
        public TokenProfile() 
        {
            CreateMap<TokenModel, AuthResponse>();
        }
    }
}
