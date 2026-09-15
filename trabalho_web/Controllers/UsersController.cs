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
        public IActionResult Post([FromBody] CreateUserDto createUserDto)
        {
           var res  =  service.CreateUserAsync(createUserDto);

            return Ok();
        }


    }
}
