namespace geniusxp_backend_dotnet.Requests
{
    public class CreateEventDayRequest
    {
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }
        public string? TransmissionUrl { get; set; }
    }
}
