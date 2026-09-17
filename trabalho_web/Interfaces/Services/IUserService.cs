using trabalho_web.Models;

namespace trabalho_web.Interfaces.Services
{
    public interface IUserService
    {

        Task<UserResponseDto> CreateUserAsync(CreateUserDto createUserDto);

        Task<List<UserDto>> GetUsersAsync();

        Task DeleteUserAsync(int id);

        Task<UserResponseDto> LoginAsync(LoginDto loginDto);
    }
}
