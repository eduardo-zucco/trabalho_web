using Microsoft.AspNetCore.Mvc;
using trabalho_web.Interfaces.Services;
using trabalho_web.Models;

namespace trabalho_web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController(IUserService service) : ControllerBase
    {
        [HttpPost(Name = "Create")]
        public async Task<IActionResult> Post([FromBody] CreateUserDto createUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await service.CreateUserAsync(createUserDto);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Ocorreu um erro interno no servidor.", details = ex.Message });
            }
        }

        [HttpGet(Name = "GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await service.GetUsers();
            return Ok(result);
        }



    }
}
