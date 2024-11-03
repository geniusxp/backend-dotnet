namespace geniusxp_backend_dotnet.Requests
{
    public class CreateUserRequest
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Cpf { get; set; }
        public required DateOnly DateOfBirth { get; set; }
        public required string AvatarUrl { get; set; }
        public string? Description { get; set; }
        public string? Interests { get; set; }
    }
}
