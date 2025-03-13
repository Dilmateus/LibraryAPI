using LibraryApi.Application.Interfaces;
using LibraryApi.Domain.Dtos;
using LibraryApi.Domain.Entities;
using LibraryApi.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace LibraryApi.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher; 

        public UserService(IRepository<User> userRepository, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        }

        public async Task<IEnumerable<User>> GetUsersAsync() => await _userRepository.GetAllAsync();
        public async Task<User> GetUserByIdAsync(Guid id) => await _userRepository.GetByIdAsync(id);
        public async Task AddUserAsync(User user)
        {
            user.PasswordHash = _passwordHasher.HashPassword(user, user.PasswordHash);
            await _userRepository.AddAsync(user);
        }
        public async Task UpdateUserAsync(User user) => await _userRepository.UpdateAsync(user);
        public async Task DeleteUserAsync(Guid id) => await _userRepository.DeleteAsync(id);

        public async Task<IEnumerable<UserDto>> GetUserForSelectAsync()
        {
            return await _userRepository.GetSelectedColumnsAsync(u => new UserDto
            {
                Name = u.Name,
                Email = u.Email
            });
        }
    }
}
