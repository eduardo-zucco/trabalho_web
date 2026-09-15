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
            if (createUserDto == null || string.IsNullOrWhiteSpace(createUserDto.Email) || string.IsNullOrWhiteSpace(createUserDto.Password))
            {
                throw new ArgumentException("Dados do usuário inválidos.");
            }

            try
            {
                bool isDuplicate = await context.Users.AnyAsync(t => t.Email == createUserDto.Email);

                if (isDuplicate)
                {
                    throw new InvalidOperationException("Email já está em uso, tente novamente!");
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
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao processar a criação do usuário.", ex);
            }
        }

        public async Task<List<UserDto>> GetUsers()
        {
            return await context.Users
                .Select(u => new UserDto
                {
                    Id = u.Id.ToString(),
                    Name = u.Name,
                    Email = u.Email
                })
                .ToListAsync();
        }
    }
}
