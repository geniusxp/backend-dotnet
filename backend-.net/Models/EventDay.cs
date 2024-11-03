using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace geniusxp_backend_dotnet.Models
{
    [Table("TB_GXP_EVENT_DAY")]
    public class EventDay
    {
        [Key]
        [Column("id_event_day")]
        public int Id { get; set; }

        [Column("dt_start")]
        public DateTime StartDate { get; set; }

        [Column("dt_end")]
        public DateTime EndDate { get; set; }

        [Column("url_transmission")]
        public string? TransmissionUrl { get; set; }

        [ForeignKey("id_event")]
        public Event Event { get; set; }

        public EventDay() { }
    }
}
