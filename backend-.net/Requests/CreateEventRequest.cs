namespace geniusxp_backend_dotnet.Requests
{
    public class CreateEventRequest
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string EventType { get; set; }
        public required string ImageUrl { get; set; }

        public required ICollection<CreateEventDayRequest> EventDays { get; set; }
        public required ICollection<CreateTicketTypeRequest> TicketTypes { get; set; }
    }
}
