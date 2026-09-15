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

            
            var result = await service.CreateUserAsync(createUserDto);


            if (result.Contains("sucesso"))
            {
                return Ok(new { message = result });
            }

            return BadRequest(new { error = result });
        }
    }
}
