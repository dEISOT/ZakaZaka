using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZakaZaka.Data;
using ZakaZaka.Models;
using ZakaZaka.Models.Request;
using ZakaZaka.Repositories.Contracts;

namespace ZakaZaka.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ZakaDbContext _db;

        public UserRepository(ZakaDbContext db)
        {
            _db = db;
        }

        public async Task<User> AddUserAsync(User user)
        {
            await _db.Users.AddAsync(user);
            _db.SaveChanges();
            return user;
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task AddUserCredential(UserCredential credential)
        {
            await _db.UserCredentials.AddAsync(credential);
            await _db.SaveChangesAsync();
        }

        public async Task<UserCredential> GetCredentialByUserIdAsync(Guid id)
        {
            return await _db.UserCredentials
                .FirstOrDefaultAsync(u => u.UserId == id);
        }
    }
}
