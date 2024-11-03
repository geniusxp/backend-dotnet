using geniusxp_backend_dotnet.Builders;
using geniusxp_backend_dotnet.Data;
using geniusxp_backend_dotnet.Requests;
using geniusxp_backend_dotnet.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace geniusxp_backend_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] 
    [SwaggerTag("Controller de Eventos")]
    public class EventsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EventsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [SwaggerOperation("Cadastra um evento com dias de evento e tipos de ingresso")]
        public async Task<ActionResult<EventSimplifiedResponse>> CreateEvent(CreateEventRequest request)
        {
            var eventBuilder = new EventBuilder();
            var eventDayBuilder = new EventDayBuilder();
            var ticketTypeBuilder = new TicketTypeBuilder();

            var newEvent = eventBuilder
                .Name(request.Name)
                .Description(request.Description)
                .EventType(request.EventType)
                .ImageUrl(request.ImageUrl)
                .Days()
                .TicketTypes()
                .Build();

            foreach (var item in request.EventDays)
            {
                if (item.EndDate < item.StartDate)
                {
                    return BadRequest("Dia de evento inválido!");
                }

                var newEventDay = eventDayBuilder
                    .StartDate(item.StartDate)
                    .EndDate(item.EndDate)
                    .TransmissionUrl(item.TransmissionUrl)
                    .Build();

                newEvent.Days.Add(newEventDay);
            }

            foreach (var item in request.TicketTypes)
            {
                var newTicketType = ticketTypeBuilder
                    .Price(item.Price)
                    .Category(item.Category)
                    .Description(item.Description)
                    .AvailableQuantity(item.AvailableQuantity)
                    .QuantitySold()
                    .Build();

                newEvent.TicketTypes.Add(newTicketType);
            }

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();

            var createdEvent = EventSimplifiedResponse.From(newEvent);

            return CreatedAtAction("FindEventById", new { id = createdEvent.Id }, createdEvent);
        }

        [HttpGet]
        [SwaggerOperation("Lista todos os eventos")]
        public async Task<ActionResult<IEnumerable<EventSimplifiedResponse>>> FindAllEvents()
        {
            return Ok(await _context.Events
                .Select(e => EventSimplifiedResponse.From(e))
                .ToListAsync());
        }

        [HttpGet("{id}")]
        [SwaggerOperation("Lista um evento por Id")]
        public async Task<ActionResult<EventDetailedResponse>> FindEventById(int id)
        {
            var foundEvent = await _context.Events
                .Include(e => e.TicketTypes)
                .Include(e => e.Days)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (foundEvent == null)
            {
                return NotFound();
            }

            return Ok(EventDetailedResponse.From(foundEvent));
        }

        [HttpDelete("{id}")]
        [SwaggerOperation("Deleta um evento por Id")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var foundEvent = await _context.Events
                .Include(e => e.TicketTypes)
                .ThenInclude(t => t.Tickets)
                .Include(e => e.Days)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (foundEvent == null)
            {
                return NotFound();
            }

            _context.Tickets.RemoveRange(foundEvent.TicketTypes.SelectMany(tt => tt.Tickets));
            _context.TicketTypes.RemoveRange(foundEvent.TicketTypes);
            _context.Events.Remove(foundEvent);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        [SwaggerOperation("Atualiza um evento")]
        public async Task<IActionResult> UpdateEvent(int id, UpdateEventRequest request)
        {
            var foundEvent = await _context.Events.FindAsync(id);

            if (foundEvent == null)
            {
                return NotFound();
            }

            foundEvent.UpdateInformation(request);
            _context.Events.Update(foundEvent);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
