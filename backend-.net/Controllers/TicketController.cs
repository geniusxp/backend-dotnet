using geniusxp_backend_dotnet.Builders;
using geniusxp_backend_dotnet.Data;
using geniusxp_backend_dotnet.Requests;
using geniusxp_backend_dotnet.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace geniusxp_backend_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] 
    [SwaggerTag("Controller de Ingressos")]
    public class TicketController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TicketController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [SwaggerOperation("Cria um novo ingresso")]
        public async Task<ActionResult<TicketSimplifiedResponse>> CreateTicket(CreateTicketRequest request)
        {
            var user = await _context.Users.FindAsync(request.UserId);
            var ticketType = await _context.TicketTypes.FindAsync(request.TicketTypeId);

            if (user == null || ticketType == null)
            {
                return NotFound();
            }

            if (ticketType.AvailableQuantity == ticketType.QuantitySold)
            {
                return BadRequest("Tipo de ingresso esgotado!");
            }

            var ticketBuilder = new TicketBuilder();

            var newTicket = ticketBuilder
                .DateIssued()
                .TicketType(ticketType)
                .User(user)
                .TicketNumber()
                .Build();

            ticketType.QuantitySold += 1;

            _context.Tickets.Add(newTicket);
            _context.TicketTypes.Update(ticketType);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateTicket), new { id = newTicket.Id }, newTicket);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation("Delete um ingresso por Id")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var foundTicket = await _context.Tickets.FindAsync(id);

            if (foundTicket == null)
            {
                return NotFound();
            }

            _context.Tickets.Remove(foundTicket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("validate/{ticketNumber}")]
        [SwaggerOperation("Valida um ingresso existente")]
        public async Task<IActionResult> ValidateTicket(Guid ticketNumber)
        {
            var foundTicket = await _context.Tickets.FirstOrDefaultAsync(t => t.TicketNumber == ticketNumber.ToString());

            if (foundTicket == null)
            {
                return NotFound();
            }

            if (foundTicket.DateOfUse != null)
            {
                return BadRequest("Ingresso já foi utilizado!");
            }

            foundTicket.DateOfUse = DateTime.Now;
            _context.Tickets.Update(foundTicket);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
