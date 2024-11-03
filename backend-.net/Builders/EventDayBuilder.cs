using geniusxp_backend_dotnet.Models;

namespace geniusxp_backend_dotnet.Builders
{
    public class EventDayBuilder
    {
        private readonly EventDay _eventDay;

        public EventDayBuilder()
        {
            _eventDay = new EventDay();
        }

        public EventDayBuilder StartDate(DateTime startDate)
        {
            _eventDay.StartDate = startDate;
            return this;
        }

        public EventDayBuilder EndDate(DateTime endDate)
        {
            _eventDay.EndDate = endDate;
            return this;
        }

        public EventDayBuilder TransmissionUrl(string? transmissionUrl)
        {
            _eventDay.TransmissionUrl = transmissionUrl;
            return this;
        }

        public EventDayBuilder Event(Event @event)
        {
            _eventDay.Event = @event;
            return this;
        }

        public EventDay Build()
        {
            return _eventDay;
        }
    }
}
