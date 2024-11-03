using geniusxp_backend_dotnet.Builders;
using geniusxp_backend_dotnet.Data;
using geniusxp_backend_dotnet.Requests;
using geniusxp_backend_dotnet.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace geniusxp_backend_dotnet.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    [SwaggerTag("Controller de Usuários")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Cria um novo usuário",
                          Description = "Requer as informações do usuário e retorna os dados do usuário criado. Para obter um token JWT, utilize o endpoint /login.")]
        public async Task<ActionResult<UserSimplifiedResponse>> CreateUser(CreateUserRequest request)
        {
            var userBuilder = new UserBuilder();

            var user = userBuilder
                .Name(request.Name)
                .Email(request.Email)
                .Password(BCrypt.Net.BCrypt.HashPassword(request.Password))
                .Cpf(request.Cpf)
                .DateOfBirth(request.DateOfBirth)
                .AvatarUrl(request.AvatarUrl)
                .Description(request.Description)
                .Interests(request.Interests)
                .Build();

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var createdUser = UserSimplifiedResponse.From(user);

            return CreatedAtAction("FindUserById", new { id = createdUser.Id }, createdUser);
        }

        [HttpGet]
        [SwaggerOperation("Lista todos os usuários")]
        public async Task<ActionResult<IEnumerable<UserSimplifiedResponse>>> FindAllUsers()
        {
            return Ok(await _context.Users
                .Select(u => UserSimplifiedResponse.From(u))
                .ToListAsync());
        }

        [HttpGet("{id}")]
        [SwaggerOperation("Lista usuário por Id")]
        public async Task<ActionResult<UserDetailedResponse>> FindUserById(int id)
        {
            var foundUser = await _context.Users
                .Include(u => u.Tickets)
                .ThenInclude(t => t.TicketType)
                .ThenInclude(tt => tt.Event)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (foundUser == null)
            {
                return NotFound();
            }

            return Ok(UserDetailedResponse.From(foundUser));
        }

        [HttpDelete("{id}")]
        [SwaggerOperation("Deleta usuário por Id")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var foundUser = await _context.Users
                .Include(u => u.Tickets)
                .ThenInclude(t => t.TicketType)
                .ThenInclude(tt => tt.Event)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (foundUser == null)
            {
                return NotFound();
            }

            _context.Tickets.RemoveRange(foundUser.Tickets);
            _context.Users.Remove(foundUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        [SwaggerOperation("Atualiza um usuário")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserRequest request)
        {
            var foundUser = await _context.Users.FindAsync(id);

            if (foundUser == null)
            {
                return NotFound();
            }

            foundUser.UpdateInformation(request);

            _context.Users.Update(foundUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
