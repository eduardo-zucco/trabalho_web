using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using trabalho_web.Interfaces.Services;
using trabalho_web.Models;

namespace trabalho_web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController(IUserService service) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody] CreateUserDto createUserDto)
        {
            var result = await service.CreateUserAsync(createUserDto);
            return StatusCode(201, ApiResponse<UserResponseDto>.Ok(result, "Usuário criado com sucesso!"));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await service.GetUsers();
            return Ok(ApiResponse<List<UserDto>>.Ok(result, "Lista de usuários obtida com sucesso."));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await service.LoginAsync(loginDto);
            return Ok(ApiResponse<UserResponseDto>.Ok(result, "Login realizado com sucesso!"));
        }


        [Authorize] 
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await service.DeleteUserAsync(id);
            return Ok(ApiResponse<bool>.Ok(true, "Usuário deletado com sucesso!"));
        }
    }
}
