using geniusxp_backend_dotnet.Controllers;
using geniusxp_backend_dotnet.Data;
using geniusxp_backend_dotnet.Models;
using geniusxp_backend_dotnet.Requests;
using geniusxp_backend_dotnet.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace geniusxp_backend_dotnet.Tests.Controllers
{
    public class UserControllerTests
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task CreateUser_ReturnsCreated_WhenUserCreatedSuccessfully()
        {
            var context = GetInMemoryDbContext();
            var controller = new UserController(context);

            var request = new CreateUserRequest
            {
                Name = "Test User",
                Email = "testuser@example.com",
                Password = "securepassword123",
                Cpf = "12345678900",
                DateOfBirth = new DateOnly(1990, 1, 1),
                AvatarUrl = "https://example.com/avatar.jpg",
                Description = "Test description",
                Interests = "Test interests"
            };

            var result = await controller.CreateUser(request);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.IsType<UserSimplifiedResponse>(createdResult.Value);
            var createdUser = (UserSimplifiedResponse)createdResult.Value;
            Assert.Equal("Test User", createdUser.Name);
        }

        [Fact]
        public async Task FindAllUsers_ReturnsOk_WhenUsersExist()
        {
            var context = GetInMemoryDbContext();
            var controller = new UserController(context);

            var user = new User
            {
                Name = "Test User",
                Email = "testuser@example.com",
                Password = "securepassword123",
                Cpf = "12345678900",
                DateOfBirth = new DateOnly(1990, 1, 1),
                AvatarUrl = "https://example.com/avatar.jpg"
            };
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();


            var result = await controller.FindAllUsers();


            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var users = Assert.IsAssignableFrom<IEnumerable<UserSimplifiedResponse>>(okResult.Value);
            Assert.Single(users);
        }

        [Fact]
        public async Task FindUserById_ReturnsOk_WhenUserExists()
        {

            var context = GetInMemoryDbContext();
            var controller = new UserController(context);

            var user = new User
            {
                Name = "Test User",
                Email = "testuser@example.com",
                Password = "securepassword123",
                Cpf = "12345678900",
                DateOfBirth = new DateOnly(1990, 1, 1),
                AvatarUrl = "https://example.com/avatar.jpg"
            };
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();


            var result = await controller.FindUserById(user.Id);


            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var foundUser = Assert.IsType<UserDetailedResponse>(okResult.Value);
            Assert.Equal(user.Name, foundUser.Name);
        }

        [Fact]
        public async Task FindUserById_ReturnsNotFound_WhenUserDoesNotExist()
        {

            var context = GetInMemoryDbContext();
            var controller = new UserController(context);


            var result = await controller.FindUserById(1); 


            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task DeleteUser_ReturnsNoContent_WhenUserDeletedSuccessfully()
        {

            var context = GetInMemoryDbContext();
            var controller = new UserController(context);

            var user = new User
            {
                Name = "Test User",
                Email = "testuser@example.com",
                Password = "securepassword123",
                Cpf = "12345678900",
                DateOfBirth = new DateOnly(1990, 1, 1),
                AvatarUrl = "https://example.com/avatar.jpg"
            };
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            var result = await controller.DeleteUser(user.Id);


            Assert.IsType<NoContentResult>(result);
            Assert.Null(await context.Users.FindAsync(user.Id)); 
        }

        [Fact]
        public async Task DeleteUser_ReturnsNotFound_WhenUserDoesNotExist()
        {
  
            var context = GetInMemoryDbContext();
            var controller = new UserController(context);


            var result = await controller.DeleteUser(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateUser_ReturnsNoContent_WhenUserUpdatedSuccessfully()
        {
 
            var context = GetInMemoryDbContext();
            var controller = new UserController(context);

            var user = new User
            {
                Name = "Test User",
                Email = "testuser@example.com",
                Password = "securepassword123",
                Cpf = "12345678900",
                DateOfBirth = new DateOnly(1990, 1, 1),
                AvatarUrl = "https://example.com/avatar.jpg"
            };
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            var updateRequest = new UpdateUserRequest
            {
                Name = "Updated User",
                Email = "updateduser@example.com",
                Password = "newpassword123",
                Cpf = "09876543211",
                DateOfBirth = new DateOnly(1995, 1, 1),
                AvatarUrl = "https://example.com/newavatar.jpg",
                Description = "Updated description",
                Interests = "Updated interests"
            };



            var result = await controller.UpdateUser(user.Id, updateRequest);


            Assert.IsType<NoContentResult>(result);
            var updatedUser = await context.Users.FindAsync(user.Id);
            Assert.Equal("Updated User", updatedUser.Name);
        }

        [Fact]
        public async Task UpdateUser_ReturnsNotFound_WhenUserDoesNotExist()
        {


            var context = GetInMemoryDbContext();
            var controller = new UserController(context);

            var updateRequest = new UpdateUserRequest
            {
                Name = "Updated User",
                Email = "updateduser@example.com",
                Password = "newpassword123",
                Cpf = "09876543211",
                DateOfBirth = new DateOnly(1995, 1, 1),
                AvatarUrl = "https://example.com/newavatar.jpg",
                Description = "Updated description",
                Interests = "Updated interests"
            };


            var result = await controller.UpdateUser(1, updateRequest); 


            Assert.IsType<NotFoundResult>(result);
        }
    }
}

