using Microsoft.AspNetCore.Mvc;
using geniusxp_backend_dotnet.Services;
using geniusxp_backend_dotnet.Models;
using geniusxp_backend_dotnet.Data; 
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.EntityFrameworkCore; 
using System.Threading.Tasks;

namespace geniusxp_backend_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Controller de Autenticação")]

    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly AppDbContext _context; 

        public AuthController(TokenService tokenService, AppDbContext context)
        {
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("login")]
        [SwaggerOperation(Summary = "Faz login e gere um token de autenticação",
                          Description = "Requer username e senha cadastrados anteriormente para autenticação. Retorna um token JWT em caso de login bem-sucedido.")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == login.Username); 


            if (user != null && VerifyPassword(login.Password, user.Password)) 
            {
                var token = _tokenService.GenerateToken(user.Name); 
                return Ok(new { token });
            }

            return Unauthorized(); 
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
