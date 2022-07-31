using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZakaZaka.Models;
using ZakaZaka.Models.Request;
using ZakaZaka.Repositories.Contracts;
using ZakaZaka.Services.Contracts;
using BCrypt.Net;

namespace ZakaZaka.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)  
        {
            _userRepository = userRepository; 
            _mapper = mapper;   
        }

        public async Task<User> AddUserAsync(UserRegisterRequest model)
        {

            User user = new User()
            {
                Email = model.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                FirstName = model.FirstName,
                LastName = model.LastName
            };
            var result = await _userRepository.AddUserAsync(user);
            UserCredential credential = new UserCredential()
            {
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                UserId = user.Id
            };
            
            await _userRepository.AddUserCredential(credential);
            return result;
        }

        public async Task<bool> CheckPasswordAsync(Guid id, string password)
        {
            var userCredential = await _userRepository.GetCredentialByUserIdAsync(id);
            return BCrypt.Net.BCrypt.Verify(password, userCredential.PasswordHash);
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _userRepository.FindByEmailAsync(email);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }
    }
}
