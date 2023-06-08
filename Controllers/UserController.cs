using backendServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace backendServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDBContext dBContext;
        public UserController(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserModel userObject)
        {
            if(userObject == null)
            {
                return BadRequest();
            }

            UserModel user = await dBContext.Users.FirstOrDefaultAsync(userOb => userOb.Email == userObject.Email && userOb.Password == userObject.Password);
            if(user == null)
            {
                return NotFound(new {Message = "El usuario y/o su contraseña son incorrectos."});
            }

            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Insert([FromBody] UserModel userObject)
        {
            if (userObject == null)
            {
                return BadRequest();
            }

            if(string.IsNullOrWhiteSpace(userObject.Email))
            {
                return BadRequest(new {Message = "Es necesario un correo electronico."});
            }

            if(string.IsNullOrEmpty(userObject.Password))
            {
                return BadRequest(new {Message = "Es necesaria su contraseña."});
            }
            if(string.IsNullOrEmpty(userObject.FirstName) || string.IsNullOrEmpty(userObject.LastName))
            {
                return BadRequest(new { Message = "Es necesario proveer su nombre completo" });
            }

            await dBContext.Users.AddAsync(userObject);
            await dBContext.SaveChangesAsync();
            return Ok(userObject);
        }
    }
}