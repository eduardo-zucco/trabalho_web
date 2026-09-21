using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using trabalho_web.Interfaces.Services;
using trabalho_web.Models;

namespace trabalho_web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IUserService service) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody] CreateUserDto createUserDto)
        {
            var result = await service.CreateUserAsync(createUserDto);
            return Created($"/api/users/{result.Id}", ApiResponse<UserResponseDto>.Ok(result, "Usuário criado com sucesso!"));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await service.GetUsersAsync();
            return Ok(ApiResponse<List<UserDto>>.Ok(result, "Lista de usuários obtida com sucesso."));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await service.LoginAsync(loginDto);
            return Ok(ApiResponse<UserResponseDto>.Ok(result, "Login realizado com sucesso!"));
        }


        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await service.GetUserByIdAsync(id);
            return Ok(ApiResponse<UserDto>.Ok(result, "Usuário obtido com sucesso."));
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto updateUserDto)
        {
            var result = await service.UpdateUserAsync(id, updateUserDto);
            return Ok(ApiResponse<UserDto>.Ok(result, "Usuário atualizado com sucesso!"));
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
