using backendServer.Helpers;
using backendServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backendServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActividadesController : ControllerBase
    {
        private readonly ApplicationDBContext dBContext;

        public ActividadesController(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] ActividadesModel actividadesObject)
        {
            if (actividadesObject == null)
            {
                return BadRequest();
            }
            actividadesObject.FechaInicio = DateTime.Now;
            await dBContext.Actividades.AddAsync(actividadesObject);
            await dBContext.SaveChangesAsync();
            return Ok(actividadesObject);
        }

        [HttpPost("Fetch")]
        public async Task<IActionResult> FetchWitListId([FromBody] int IdLista)
        {
            if (IdLista == 0)
            {
                return BadRequest();
            }
            List<ActividadesModel> actividades = await dBContext.Actividades
                .Where(actObj => actObj.IdLista == IdLista)
                .ToListAsync();

            return Ok(actividades);
        }

        [HttpPatch("update")]
        public async Task<IActionResult> Update([FromBody] ActividadesModel actividadesObject)
        {
            if(actividadesObject == null)
            {
                return BadRequest();
            }

            ActividadesModel actividadUpdated = await dBContext.Actividades.FindAsync(actividadesObject.IdActividad);
            actividadUpdated.Activo = actividadesObject.Activo;
            if(!actividadUpdated.Activo)
            {
                actividadUpdated.FechaFin = DateTime.Now;
            }
            actividadUpdated.Actividad = actividadesObject.Actividad;
            try
            {
                await dBContext.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                return Conflict();
            }
            return Ok(actividadUpdated);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] int IdActividad)
        {
            if (IdActividad == 0)
            {
                return BadRequest();
            }
            ActividadesModel actividades = await dBContext.Actividades.FindAsync(IdActividad);
            if (actividades == null)
            {
                return NotFound();
            }

            dBContext.Actividades.Remove(actividades);
            await dBContext.SaveChangesAsync();
            return Ok();

        }
    }
}
