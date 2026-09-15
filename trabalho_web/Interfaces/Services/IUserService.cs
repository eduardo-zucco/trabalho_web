using trabalho_web.Models;

namespace trabalho_web.Interfaces.Services
{
    public interface IUserService
    {

        Task<string> CreateUserAsync(CreateUserDto createUserDto);

        Task<List<UserDto>> GetUsers();
    }
}
