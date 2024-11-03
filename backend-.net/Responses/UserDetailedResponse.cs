using geniusxp_backend_dotnet.Models;

namespace geniusxp_backend_dotnet.Responses
{
    public class UserDetailedResponse
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Cpf { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Description { get; set; }
        public string? Interests { get; set; }
        public ICollection<TicketDetailedResponse>? Tickets { get; set; }

        public static UserDetailedResponse From (User user)
        {
            var response = new UserDetailedResponse();
            response.Name = user.Name;
            response.Email = user.Email;
            response.Cpf = user.Cpf;
            response.DateOfBirth = user.DateOfBirth;
            response.AvatarUrl = user.AvatarUrl;
            response.Description = user.Description;
            response.Interests = user.Interests;
            response.Tickets = TicketDetailedResponse.From(user.Tickets);
            return response;
        }
    }
}
