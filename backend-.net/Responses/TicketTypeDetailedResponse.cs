using geniusxp_backend_dotnet.Models;

namespace geniusxp_backend_dotnet.Responses
{
    public class TicketTypeDetailedResponse
    {
        public int Id { get; set; }
        public float? Price { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public int? AvailableQuantity { get; set; }
        public int? QuantitySold { get; set; }
        public EventSimplifiedResponse? Event { get; set; }

        public static TicketTypeDetailedResponse From(TicketType ticketType)
        {
            var response = new TicketTypeDetailedResponse();
            response.Id = ticketType.Id;
            response.Price = ticketType.Price;
            response.Category = ticketType.Category;    
            response.Description = ticketType.Description;
            response.AvailableQuantity = ticketType.AvailableQuantity;
            response.QuantitySold = ticketType.QuantitySold;
            response.Event = EventSimplifiedResponse.From(ticketType.Event);
            return response;
        }
    }
}
