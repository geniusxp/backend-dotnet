using geniusxp_backend_dotnet.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace geniusxp_backend_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [SwaggerTag("Controller de Tipos de Ingresso")]
    public class TicketTypesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TicketTypesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpDelete("{id}")]
        [SwaggerOperation("Deleta um tipo de ingresso por Id")]
        public async Task<IActionResult> DeleteTicketType(int id)
        {
            var foundTicketType = await _context.TicketTypes
                .Include(tt => tt.Tickets)
                .FirstOrDefaultAsync(tt => tt.Id == id);

            if (foundTicketType == null)
            {
                return NotFound();
            }

            _context.Tickets.RemoveRange(foundTicketType.Tickets);
            _context.TicketTypes.Remove(foundTicketType);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
