using geniusxp_backend_dotnet.Controllers;
using geniusxp_backend_dotnet.Data;
using geniusxp_backend_dotnet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using geniusxp_backend_dotnet.Requests;

public class TicketControllerTests
{
    private AppDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    [Fact]
    public async Task CreateTicket_ReturnsCreated_WhenTicketCreatedSuccessfully()
    {
        var context = GetInMemoryDbContext();
        var controller = new TicketController(context);

        var user = new User
        {
            Id = 1,
            Name = "Test User",
            Email = "testuser@example.com",
            Password = "securepassword123",
            Cpf = "12345678900",
            DateOfBirth = new DateOnly(1990, 1, 1),
            AvatarUrl = "https://example.com/avatar.jpg"
        };

        var ticketType = new TicketType
        {
            Id = 1,
            AvailableQuantity = 100,
            QuantitySold = 0,
            Category = "General Admission",
            Description = "Standard ticket for general admission"
        };

        await context.Users.AddAsync(user);
        await context.TicketTypes.AddAsync(ticketType);
        await context.SaveChangesAsync();

        var request = new CreateTicketRequest { UserId = 1, TicketTypeId = 1 };

        var result = await controller.CreateTicket(request);

        Assert.IsType<CreatedAtActionResult>(result.Result);
    }

    [Fact]
    public async Task DeleteTicket_ReturnsNoContent_WhenTicketDeletedSuccessfully()
    {
        var context = GetInMemoryDbContext();
        var controller = new TicketController(context);

        var user = new User
        {
            Id = 1,
            Name = "Test User",
            Email = "testuser@example.com",
            Password = "securepassword123",
            Cpf = "12345678900",
            DateOfBirth = new DateOnly(1990, 1, 1),
            AvatarUrl = "https://example.com/avatar.jpg"
        };

        var ticketType = new TicketType
        {
            Id = 1,
            AvailableQuantity = 100,
            QuantitySold = 0,
            Category = "General Admission",
            Description = "Standard ticket for general admission"
        };

        var ticket = new Ticket
        {
            Id = 1,
            TicketNumber = "1234",
            User = user,
            TicketType = ticketType
        };

        await context.Users.AddAsync(user);
        await context.TicketTypes.AddAsync(ticketType);
        await context.Tickets.AddAsync(ticket);
        await context.SaveChangesAsync();

        var result = await controller.DeleteTicket(1);

        Assert.IsType<NoContentResult>(result);
    }
}

