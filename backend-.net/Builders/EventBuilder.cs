using geniusxp_backend_dotnet.Models;

namespace geniusxp_backend_dotnet.Builders
{
    public class EventBuilder
    {
        private readonly Event _event;

        public EventBuilder()
        {
            _event = new Event();
        }

        public EventBuilder Name(string name)
        {
            _event.Name = name;
            return this;
        }

        public EventBuilder Description(string description)
        {
            _event.Description = description;
            return this;
        }

        public EventBuilder EventType(string eventType)
        {
            _event.EventType = eventType;
            return this;
        }

        public EventBuilder ImageUrl(string imageUrl)
        {
            _event.ImageUrl = imageUrl;
            return this;
        }

        public EventBuilder TicketTypes()
        {
            _event.TicketTypes = new List<TicketType>();
            return this;
        }

        public EventBuilder Days()
        {
            _event.Days = new List<EventDay>();
            return this;
        }

        public Event Build()
        {
            return _event;
        }
    }
}
