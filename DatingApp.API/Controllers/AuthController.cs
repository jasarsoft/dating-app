using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository repo;

        public AuthController(IAuthRepository repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Register(string username, string password)
        {
            //validate request
            username = username.ToLower();

            if (await repo.UserExists(username))
                return BadRequest("Username already exists");
            
            var userToCreate = new User
            {
                Username = username
            };

            var createdUser = await repo.Register(userToCreate, password);

            return StatusCode(201); 
        }
    }
}