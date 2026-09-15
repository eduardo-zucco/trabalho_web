using trabalho_web.Models;

namespace trabalho_web.Interfaces.Services
{
    public interface IUserService
    {

        Task<UserResponseDto> CreateUserAsync(CreateUserDto createUserDto);

        Task<List<UserDto>> GetUsers();
    }
}
