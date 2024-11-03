using geniusxp_backend_dotnet.Models;

namespace geniusxp_backend_dotnet.Builders
{
    public class TicketBuilder
    {
        private readonly Ticket _ticket;

        public TicketBuilder()
        {
            _ticket = new Ticket();
        }

        public TicketBuilder DateIssued()
        {
            _ticket.DateIssued = DateTime.Now;
            return this;
        }

        public TicketBuilder TicketNumber()
        {
            _ticket.TicketNumber = Guid.NewGuid().ToString();
            return this;
        }

        public TicketBuilder User(User user)
        {
            _ticket.User = user;
            return this;
        }

        public TicketBuilder TicketType(TicketType ticketType)
        {
            _ticket.TicketType = ticketType;
            return this;
        }

        public Ticket Build()
        {
            return _ticket;
        }
    }
}
