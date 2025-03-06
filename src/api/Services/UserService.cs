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

        public async Task<(User? User, string? ErrorMessage)> CreateUserAsync(CreateUserDto userDto)
        {
            // Check if email already exists
            bool emailExists = await _context.Users.AnyAsync(u => u.Email == userDto.Email);
            if (emailExists)
            {
                return (null, "Email já foi utilizado");
            }

            var user = new User(
                userDto.Username,
                userDto.Email,
                BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                userDto.DateOfBirth
            );

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return (user, null);
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<(User? User, string? ErrorMessage)> UpdateUserAsync(int id, UpdateUserDto userDto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return (null, "Usuário não encontrado");

            user.Username = userDto.Username;

            // Check if email is changed and if the new email already exists
            if (userDto.Email != null && userDto.Email != user.Email)
            {
                bool emailExists = await _context.Users.AnyAsync(u => u.Email == userDto.Email);
                if (emailExists)
                {
                    return (null, "Email já foi utilizado");
                }
                user.Email = userDto.Email;
            }

            if (userDto.DateOfBirth.HasValue) user.DateOfBirth = userDto.DateOfBirth.Value;

            await _context.SaveChangesAsync();
            return (user, null);
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