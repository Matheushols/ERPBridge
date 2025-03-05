using ERPBridge.Class;
using ERPBridge.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ERPBridge.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUserAsync(CreateUserDto userDto)
        {
            // Você pode adicionar validações adicionais aqui
            var user = new User(
                userDto.Username, 
                userDto.Email, 
                BCrypt.Net.BCrypt.HashPassword(userDto.Password), 
                userDto.DateOfBirth
            );

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> UpdateUserAsync(int id, UpdateUserDto userDto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            user.Username = userDto.Username;
            if (userDto.Email != null) user.Email = userDto.Email;
            if (userDto.DateOfBirth.HasValue) user.DateOfBirth = userDto.DateOfBirth.Value;

            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}