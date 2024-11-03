using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace geniusxp_backend_dotnet.Models
{
    [Table("TB_GXP_TICKET")]
    [Index(nameof(TicketNumber), IsUnique = true)]
    public class Ticket
    {
        [Key]
        [Column("id_ticket")]
        public int Id { get; set; }

        [Column("dt_use")]
        public DateTime? DateOfUse { get; set; }

        [Column("dt_issued")]
        public DateTime DateIssued { get; set; }

        [Column("nr_ticket")]
        public string TicketNumber { get; set; }

        [ForeignKey("id_ticket_type")]
        public TicketType TicketType { get; set; }

        [ForeignKey("id_user")]
        public User User { get; set; }

        public Ticket() { }

    }
}
