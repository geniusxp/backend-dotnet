using geniusxp_backend_dotnet.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace geniusxp_backend_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [SwaggerTag("Controller de Dias de Evento")]
    public class EventDaysController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EventDaysController(AppDbContext context)
        {
            _context = context;
        }

        [HttpDelete("{id}")]
        [SwaggerOperation("Deleta um dia de evento")]
        public async Task<IActionResult> DeleteEventDay(int id)
        {
            var foundEventDay = await _context.EventsDay.FindAsync(id);

            if (foundEventDay == null)
            {
                return NotFound();
            }

            _context.EventsDay.Remove(foundEventDay);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
