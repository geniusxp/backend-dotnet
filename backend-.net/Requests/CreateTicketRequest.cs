namespace geniusxp_backend_dotnet.Requests
{
    public class CreateTicketRequest
    {
        public required int UserId { get; set; }
        public required int TicketTypeId { get; set; }
    }
}
