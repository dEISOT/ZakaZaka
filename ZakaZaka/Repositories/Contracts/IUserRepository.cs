using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZakaZaka.Models;
using ZakaZaka.Models.Request;

namespace ZakaZaka.Repositories.Contracts
{
    public interface IUserRepository
    {
        Task<User> FindByEmailAsync(string email);
        Task<User> AddUserAsync(User user);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddUserCredential(UserCredential credential);
        Task<UserCredential> GetCredentialByUserIdAsync(Guid id);
    }
}
