using geniusxp_backend_dotnet.Models;

namespace geniusxp_backend_dotnet.Responses
{
    public class TicketDetailedResponse
    {
        public int Id { get; set; }
        public DateTime? DateOfUse { get; set; }
        public DateTime? DateIssued { get; set; }
        public string? TicketNumber { get; set; }
        public TicketTypeDetailedResponse? TicketType { get; set; }

        public static ICollection<TicketDetailedResponse> From (ICollection<Ticket> Tickets)
        {
            return Tickets.Select(ticket => new TicketDetailedResponse
            {
                Id = ticket.Id,
                DateIssued = ticket.DateIssued,
                TicketNumber = ticket.TicketNumber,
                DateOfUse = ticket.DateOfUse,
                TicketType = TicketTypeDetailedResponse.From(ticket.TicketType)
            }).ToList();
        }
    }
}
