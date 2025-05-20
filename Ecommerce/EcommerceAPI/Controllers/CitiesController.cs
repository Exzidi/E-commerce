using Ecommerce.API.Data;
using Ecommerce.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
            private readonly DataContext _context;
            public CitiesController(DataContext context)
            {
                _context = context;
            }
            [HttpGet]
            public async Task<ActionResult> GetAsync()
            {
                return Ok(await _context.Cities.ToListAsync());
            }

            [HttpPost]
            public async Task<ActionResult> PostAsync(City city)
            {
                try
                {
                    _context.Update(city);
                    await _context.SaveChangesAsync();
                    return Ok(city);
                }

                catch (DbUpdateException update)
                {
                    if (update.InnerException.Message.Contains("duplicate")) return BadRequest("Ya hay un registro con el mismo nombre");

                    return BadRequest(update.InnerException.Message);
                }

                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }


            }
            [HttpPut]
            public async Task<ActionResult> PutAsync(City city)
            {
                try
                {
                    _context.Update(city);
                    await _context.SaveChangesAsync();
                    return Ok(city);
                }

                catch (DbUpdateException update)
                {
                    if (update.InnerException.Message.Contains("duplicate")) return BadRequest("Ya hay un registro con el mismo nombre");

                    return BadRequest(update.InnerException.Message);
                }

                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            [HttpDelete(".{id:int}")]
            public async Task<ActionResult> DeleteAsync(int id)
            {
                var afectedRows = await _context.States.Where(c => c.Id == id).ExecuteDeleteAsync();
                if (afectedRows == 0)
                {
                    return NotFound();
                }
                return NoContent();
            }
    }
}
