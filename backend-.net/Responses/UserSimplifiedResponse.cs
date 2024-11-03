using geniusxp_backend_dotnet.Models;

namespace geniusxp_backend_dotnet.Responses
{
    public class UserSimplifiedResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Description { get; set; }
        public string? Interests { get; set; }

        public static UserSimplifiedResponse From(User user)
        {
            var response = new UserSimplifiedResponse();
            response.Id = user.Id;
            response.Name = user.Name;
            response.AvatarUrl = user.AvatarUrl;
            response.Description = user.Description;
            response.Interests = user.Interests;
            return response;
        }
    }
}
