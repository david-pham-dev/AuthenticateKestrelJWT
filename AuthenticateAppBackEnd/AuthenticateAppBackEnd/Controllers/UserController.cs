using AuthenticateAppBackEnd.Data;
using AuthenticateAppBackEnd.DTOs;
using AuthenticateAppBackEnd.Model.Entity;
using AuthenticateAppBackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AuthenticateAppBackEnd.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly JwtService _jwtService;
        private readonly HashPasswordService _hashPasswordService;
        private readonly DbService _dbService;
        public UserController(ApplicationDbContext dbContext, JwtService jwtService ,HashPasswordService hashPasswordService, DbService dbService )
        {
            _dbContext = dbContext;
            _jwtService = jwtService;
            _hashPasswordService = hashPasswordService;
            _dbService = dbService;
        }

        //register
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] DTOs.RegisterRequest registerRequest)
        {   
            if(await _dbService.AddUserToDatabase(registerRequest) == false)
            {
                return BadRequest("Email Already Exist! Please Try Another Email");
            }

            //if (await _dbService.CheckDuplicateEmail(registerRequest))
            //{
            //    return BadRequest("Email Already Exist! Please Try Another Email");
            //}
            //var user = new User 
            //{   Email = registerRequest.Email,
            //    Password = _hashPasswordService.HashPassword(registerRequest.Password),
            //    Name = registerRequest.Name,
            //};
            //_dbContext.Users.Add(user);
            //await _dbContext.SaveChangesAsync();
            else
            {
                return Ok(new { message = "Successfully registered new user" });
            }
            
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseModel?>> Login([FromBody] DTOs.LoginRequest loginRequest)
        {
            var result = await _jwtService.Authenticate(loginRequest);
            if (result == null)
                return Unauthorized();
            return result;
        }   

        [HttpGet("data")]
        [Authorize]
        public async Task<ActionResult<User?>> GetUserData()
         {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the user ID from the token
            if (userId == null)
            {
                return Unauthorized("Invalid token");
            }          
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == int.Parse(userId));

            if (user == null)
                return NotFound("User not found");

            return Ok(user); // Return user data
        }


    }
}
