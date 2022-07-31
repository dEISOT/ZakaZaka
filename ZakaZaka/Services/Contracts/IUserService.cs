using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZakaZaka.Models;
using ZakaZaka.Models.Request;

namespace ZakaZaka.Services.Contracts
{
    public interface IUserService
    {
        Task<User> FindByEmailAsync(string email);
        Task<User> AddUserAsync(UserRegisterRequest model);
        Task<IEnumerable<User>> GetAllAsync();
        Task<bool> CheckPasswordAsync(Guid id, string password);
    }
}
