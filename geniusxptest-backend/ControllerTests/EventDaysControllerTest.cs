using geniusxp_backend_dotnet.Controllers;
using geniusxp_backend_dotnet.Data;
using geniusxp_backend_dotnet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class EventDaysControllerTests
{
    private AppDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task DeleteEventDay_ReturnsNoContent_WhenEventDayExists()
    {
        var context = GetInMemoryDbContext();
        var controller = new EventDaysController(context);

        var eventInstance = new Event { Id = 1, Name = "Sample Event", Description = "Description", EventType = "Type", ImageUrl = "http://example.com" };
        var eventDay = new EventDay { Id = 1, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1), Event = eventInstance };

        context.Events.Add(eventInstance);
        context.EventsDay.Add(eventDay);
        await context.SaveChangesAsync();

        var result = await controller.DeleteEventDay(1);

        Assert.IsType<NoContentResult>(result);
        Assert.Null(await context.EventsDay.FindAsync(1));
    }

    [Fact]
    public async Task DeleteEventDay_ReturnsNotFound_WhenEventDayDoesNotExist()
    {
        var context = GetInMemoryDbContext();
        var controller = new EventDaysController(context);

        var result = await controller.DeleteEventDay(99);

        Assert.IsType<NotFoundResult>(result);
    }
}
