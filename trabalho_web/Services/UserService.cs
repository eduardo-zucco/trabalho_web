using Microsoft.EntityFrameworkCore;
using trabalho_web.Entities;
using trabalho_web.Interfaces.Services;
using trabalho_web.Models;

namespace trabalho_web.Services
{
    public class UserService(AppDbContext context, JwtService jwtService) : IUserService
    {


        public async Task<string> CreateUserAsync(CreateUserDto createUserDto)
        {

            var passwordHash = PasswordHasher.HashPassword(createUserDto.Password);

            UserEntity newUser = new UserEntity(createUserDto.UserName, createUserDto.Email, passwordHash);

            if (newUser == null)
            {
                return "não pode vazio";
            }

            bool isDuplicate = await context.Usuarios.AnyAsync(t => t.Email == newUser.Email);

            if (isDuplicate)
            {
                return "Email já esta em uso, tente novamente!";
            }


            context.Usuarios.Add(newUser);
            await context.SaveChangesAsync();


            return $"criado com sucesso o usuario: {newUser.Name} ";

        }



    }
}
