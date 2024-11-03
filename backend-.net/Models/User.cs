using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using geniusxp_backend_dotnet.Requests;

namespace geniusxp_backend_dotnet.Models
{
    [Table("TB_GXP_USER")]
    [Index(nameof(Cpf), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        [Key]
        [Column("id_user")]
        public int Id { get; set; }

        [Column("ds_name")]
        public string Name { get; set; }

        [Column("ds_email")]
        public string Email { get; set; }

        [Column("ds_password")]
        public string Password { get; set; }

        [Column("nr_cpf")]
        public string Cpf { get; set; }

        [Column("dt_birth")]
        public DateOnly DateOfBirth { get; set; }

        [Column("url_avatar")]
        public string AvatarUrl { get; set; }

        [Column("tx_description")]
        public string? Description { get; set; }

        [Column("tx_interests")]
        public string? Interests { get; set; }

        public ICollection<Ticket> Tickets { get; set; }

        public User() { }

        public void UpdateInformation(UpdateUserRequest request)
        {
            if (request.Interests != null)
            {
                Interests = request.Interests;
            }

            if (request.Cpf != null)
            {
                Cpf = request.Cpf;
            }

            if (request.DateOfBirth != null)
            {
                DateOfBirth = (DateOnly) request.DateOfBirth;
            }

            if (request.Email != null) 
            {
                Email = request.Email;
            }

            if (request.AvatarUrl != null)
            {
                AvatarUrl = request.AvatarUrl;
            }

            if (request.Description != null)
            {
                Description = request.Description;
            }

            if (request.Name != null)
            {
                Name = request.Name;
            }

            if (request.Password != null)
            {
                Password = request.Password;
            }
        }
    }
}
