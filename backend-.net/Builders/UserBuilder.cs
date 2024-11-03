using geniusxp_backend_dotnet.Models;

namespace geniusxp_backend_dotnet.Builders
{
    public class UserBuilder
    {
        private readonly User _user;

        public UserBuilder()
        {
            _user = new User();
        }

        public UserBuilder Name (string name)
        {
            _user.Name = name;
            return this;
        }
        
        public UserBuilder Email(string email)
        {
            _user.Email = email;
            return this;
        }

        public UserBuilder Password(string password)
        {
            _user.Password = password;  
            return this;
        }

        public UserBuilder Cpf(string cpf) 
        {
            _user.Cpf = cpf;
            return this;
        }

        public UserBuilder DateOfBirth(DateOnly dateOfBirth)
        {
            _user.DateOfBirth = dateOfBirth;
            return this;
        }

        public UserBuilder AvatarUrl(string avatarUrl)
        {
            _user.AvatarUrl = avatarUrl;
            return this;
        }

        public UserBuilder Description(string? description)
        {
            if (description != null)
            {
                _user.Description = description;
            }
            
            return this;
        }

        public UserBuilder Interests(string? interests)
        {
            if (interests != null)
            {
                _user.Interests = interests;
            }

            return this;
        }

        public User Build()
        {
            return _user;
        }
    }
}
