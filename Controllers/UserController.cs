using backendServer.Helpers;
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

            UserModel user = await dBContext.Users.FirstOrDefaultAsync(userOb => userOb.Email == userObject.Email);
            if(user == null)
            {
                return NotFound(new {Message = "El correo electronico y/o su contraseña son incorrectos."});
            }

            if(!PasswordHashing.VerifyPassword(userObject.Password, user.Password))
            {
                return BadRequest(new { Message = "El correo electronico y/o contraseña son incorrectos." });
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
            if(await CheckIfEmailExistsAsync(userObject.Email))
            {
                return BadRequest(new { Mesage = "Este correo ya está asociado a otro usuario." });
            }
            if(string.IsNullOrEmpty(userObject.Password))
            {
                return BadRequest(new {Message = "Es necesaria su contraseña."});
            }
            userObject.Password = PasswordHashing.HashedPassword(userObject.Password);
            if(string.IsNullOrEmpty(userObject.FirstName) || string.IsNullOrEmpty(userObject.LastName))
            {
                return BadRequest(new { Message = "Es necesario proveer su nombre completo" });
            }

            await dBContext.Users.AddAsync(userObject);
            await dBContext.SaveChangesAsync();
            return Ok(userObject);
        }

        private async Task<bool> CheckIfEmailExistsAsync(string email)
        {
          return await dBContext.Users.AnyAsync(user => user.Email == email);
        }
    }
}