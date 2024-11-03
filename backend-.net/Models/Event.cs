using geniusxp_backend_dotnet.Requests;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace geniusxp_backend_dotnet.Models
{
    [Table("TB_GXP_EVENT")]
    public class Event
    {
        [Key]
        [Column("id_event")]
        public int Id { get; set; }

        [Column("nm_event")]
        public string Name { get; set; }

        [Column("tx_description")]
        public string Description { get; set; }

        [Column("ds_event_type")]
        public string EventType { get; set; }

        [Column("url_image")]
        public string ImageUrl { get; set; }

        public ICollection<EventDay> Days { get; set; }

        public ICollection<TicketType> TicketTypes { get; set; }

        public Event() { }

        public void UpdateInformation(UpdateEventRequest request)
        {
            if (request.EventType != null)
            {
                EventType = request.EventType;
            }

            if (request.ImageUrl != null)
            {
                ImageUrl = request.ImageUrl;
            }

            if (request.Name != null)
            {
                Name = request.Name;
            }

            if (request.Description != null)
            {
                Description = request.Description;
            }
        }
    }
}
