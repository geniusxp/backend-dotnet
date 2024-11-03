using geniusxp_backend_dotnet.Models;

namespace geniusxp_backend_dotnet.Builders
{
    public class TicketTypeBuilder
    {
        private readonly TicketType _ticketType;

        public TicketTypeBuilder()
        {
            _ticketType = new TicketType();
        }

        public TicketTypeBuilder Price(float price)
        {
            _ticketType.Price = price;
            return this;
        }

        public TicketTypeBuilder Category(string category)
        {
            _ticketType.Category = category;
            return this;
        }

        public TicketTypeBuilder Description(string description)
        {
            _ticketType.Description = description;
            return this;
        }

        public TicketTypeBuilder AvailableQuantity(int availableQuantity)
        {
            _ticketType.AvailableQuantity = availableQuantity;
            return this;
        }

        public TicketTypeBuilder QuantitySold()
        {
            _ticketType.QuantitySold = 0;
            return this;
        }

        public TicketTypeBuilder Event(Event @event) {
            _ticketType.Event = @event;
            return this;
        }

        public TicketType Build()
        {
            return _ticketType;
        }
    }
}
