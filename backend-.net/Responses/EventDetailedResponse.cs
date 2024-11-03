using geniusxp_backend_dotnet.Models;

namespace geniusxp_backend_dotnet.Responses
{
    public class EventDetailedResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? EventType { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<TicketTypeSimplifiedResponse>? TicketTypes { get; set; }
        public ICollection<EventDaySimplifiedResponse>? EventDays { get; set; }

        public static EventDetailedResponse From(Event @event)
        {
            var response = new EventDetailedResponse();
            response.Id = @event.Id;
            response.Name = @event.Name;
            response.Description = @event.Description;
            response.EventType = @event.EventType;
            response.ImageUrl = @event.ImageUrl;
            response.TicketTypes = TicketTypeSimplifiedResponse.From(@event.TicketTypes);
            response.EventDays = EventDaySimplifiedResponse.From(@event.Days);
            return response;
        }
    }
}
