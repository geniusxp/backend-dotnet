namespace geniusxp_backend_dotnet.Requests
{
    public class CreateTicketTypeRequest
    {
        public required float Price { get; set; }
        public required string Category { get; set; }
        public required string Description { get; set; }
        public required int AvailableQuantity { get; set; }
    }
}
