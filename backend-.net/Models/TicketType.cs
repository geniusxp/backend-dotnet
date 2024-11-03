using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace geniusxp_backend_dotnet.Models
{
    [Table("TB_GXP_TICKET_TYPE")]
    public class TicketType
    {
        [Key]
        [Column("id_ticket_type")]
        public int Id { get; set; }

        [Column("vl_price")]
        public float Price { get; set; }

        [Column("ds_category")]
        public string Category { get; set; }

        [Column("tx_description")]
        public string Description { get; set; }

        [Column("nr_quantity")]
        public int AvailableQuantity { get; set; }

        [Column("nr_sold")]
        public int QuantitySold { get; set; }

        [Column("dt_finished_at")]
        public DateTime FinishedAt { get; set; }

        [ForeignKey("id_event")]
        public Event Event { get; set; }

        public ICollection<Ticket> Tickets { get; set; }

        public TicketType() { }
    }
}
