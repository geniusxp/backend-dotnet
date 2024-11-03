using geniusxp_backend_dotnet.Models;

namespace geniusxp_backend_dotnet.Responses
{
    public class TicketSimplifiedResponse
    {
        public int? Id { get; set; }
        public DateTime? DateOfUse { get; set; }
        public DateTime? DateIssued { get; set; }
        public string? TicketNumber { get; set; }

        public static TicketSimplifiedResponse From(Ticket ticket)
        {
            var response = new TicketSimplifiedResponse();
            response.Id = ticket.Id;
            response.DateOfUse = ticket.DateOfUse;
            response.DateIssued = ticket.DateIssued;
            response.TicketNumber = ticket.TicketNumber;
            return response;
        }
    }
}
