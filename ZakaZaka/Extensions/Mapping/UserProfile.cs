using AutoMapper;
using ZakaZaka.Models;
using ZakaZaka.Models.Request;

namespace ZakaZaka.Extensions.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegisterRequest, User>();
        }
    }
}
