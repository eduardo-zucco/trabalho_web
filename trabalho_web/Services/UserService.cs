using trabalho_web.Entities;
using trabalho_web.Interfaces.Services;
using trabalho_web.Models;

namespace trabalho_web.Services
{
    public class UserService(AppDbContext context) : IUserService
    { 
      

        public  async Task <string> CreateUserAsync(CreateUserDto createUserDto)
        {

            UserEntity newUser = new UserEntity(createUserDto.UserName, createUserDto.Email, createUserDto.Password);
            if (newUser == null)
            {
                return "não pode vazio";
            }


            context.Usuarios.Add(newUser);
            await context.SaveChangesAsync();

            return $"criado com sucesso o usuario: {newUser.Name} ";

        }
    }
}
