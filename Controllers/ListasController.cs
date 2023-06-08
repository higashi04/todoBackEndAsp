using backendServer.Helpers;
using backendServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backendServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListasController : ControllerBase
    {
        private readonly ApplicationDBContext dBContext;

        public ListasController(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] ListasModel listaObject)
        {
            if (listaObject == null)
            {
                return BadRequest();
            }
            await dBContext.Listas.AddAsync(listaObject); 
            await dBContext.SaveChangesAsync();
            return Ok(listaObject);
        }

        [HttpPost("Fetch")]
        public async Task<IActionResult> FetchWithUserId([FromBody] int IdUsuario)
        {
            if(IdUsuario == 0)
            {
                return BadRequest();
            }
            List<ListasModel> listas = await dBContext.Listas
                .Where(listObj => listObj.IdUsuario == IdUsuario)
                .ToListAsync();

            return Ok(listas);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] int IdLista)
        {
            if(IdLista == 0)
            {
                return BadRequest();
            }
            ListasModel listasModel = await dBContext.Listas.FindAsync(IdLista);
            if (listasModel == null)
            {
                return NotFound();
            }

            dBContext.Listas.Remove(listasModel);
            await dBContext.SaveChangesAsync();
            return Ok();

        }
    }
}
