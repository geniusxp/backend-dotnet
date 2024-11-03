using geniusxp_backend_dotnet.Models;

namespace geniusxp_backend_dotnet.Responses
{
    public class TicketTypeSimplifiedResponse
    {
        public int Id { get; set; }
        public float? Price { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public int? AvailableQuantity { get; set; }
        public int? QuantitySold { get; set; }

        public static ICollection<TicketTypeSimplifiedResponse> From(ICollection<TicketType> ticketTypes)
        {
            return ticketTypes.Select(ticketType => new TicketTypeSimplifiedResponse
            {
                Id = ticketType.Id,
                Price = ticketType.Price,
                Category = ticketType.Category,
                Description = ticketType.Description,
                AvailableQuantity = ticketType.AvailableQuantity,
                QuantitySold = ticketType.QuantitySold
            }).ToList();
        }
    }
}
