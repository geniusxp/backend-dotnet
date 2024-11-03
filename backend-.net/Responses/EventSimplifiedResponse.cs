using geniusxp_backend_dotnet.Models;

namespace geniusxp_backend_dotnet.Responses
{
    public class EventSimplifiedResponse
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? EventType { get; set; }
        public string? ImageUrl { get; set; }

        public static EventSimplifiedResponse From (Event model)
        {
            var response = new EventSimplifiedResponse();
            response.Id = model.Id;
            response.Name = model.Name;
            response.Description = model.Description;
            response.EventType = model.EventType;
            response.ImageUrl = model.ImageUrl;
            return response;
        }
    }
}
