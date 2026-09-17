using Microsoft.EntityFrameworkCore;
using trabalho_web.Entities;
using trabalho_web.Interfaces.Services;
using trabalho_web.Models;

namespace trabalho_web.Services
{
    public class UserService(AppDbContext context, IJwtService jwtService) : IUserService
    {
        public async Task<UserResponseDto> CreateUserAsync(CreateUserDto createUserDto)
        {
            if (createUserDto == null || string.IsNullOrWhiteSpace(createUserDto.Email) || string.IsNullOrWhiteSpace(createUserDto.Password) || string.IsNullOrWhiteSpace(createUserDto.UserName))
            {
                throw new ArgumentException("Dados do usuário são obrigatórios.");
            }

            bool isDuplicate = await context.Users.AnyAsync(t => t.Email == createUserDto.Email);

            if (isDuplicate)
            {
                throw new InvalidOperationException("E-mail já está em uso, tente novamente!");
            }

            var passwordHash = PasswordHasher.HashPassword(createUserDto.Password);
            UserEntity newUser = new UserEntity(createUserDto.UserName, createUserDto.Email, passwordHash);

            context.Users.Add(newUser);
            await context.SaveChangesAsync();

            var token = jwtService.GenerateToken(newUser.Id, newUser.Name);

            return new UserResponseDto
            {
                Id = newUser.Id,
                Name = newUser.Name,
                Email = newUser.Email,
                Token = token
            };
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await context.Users.FindAsync(id);

            if (user == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }

            context.Users.Remove(user);
            await context.SaveChangesAsync();
        }

        public async Task<List<UserDto>> GetUsersAsync()
        {
            return await context.Users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email
                })
                .ToListAsync();
        }

        public async Task<UserResponseDto> LoginAsync(LoginDto loginDto)
        {
            if (loginDto == null || string.IsNullOrWhiteSpace(loginDto.Email) || string.IsNullOrWhiteSpace(loginDto.Password))
            {
                throw new ArgumentException("E-mail e senha são obrigatórios.");
            }

            var user = await context.Users.FirstOrDefaultAsync(t => t.Email == loginDto.Email);

            if (user == null || !PasswordHasher.VerifyPassword(loginDto.Password, user.Password))
            {
                throw new UnauthorizedAccessException("E-mail ou senha inválidos.");
            }

            var token = jwtService.GenerateToken(user.Id, user.Name);

            return new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Token = token
            };
        }
    }
}
