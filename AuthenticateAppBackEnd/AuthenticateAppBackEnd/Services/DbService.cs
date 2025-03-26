using AuthenticateAppBackEnd.Data;
using AuthenticateAppBackEnd.Model.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace AuthenticateAppBackEnd.Services
{
    public class DbService
    { private readonly ApplicationDbContext _dbContext;
        private readonly HashPasswordService _hashPasswordService;
        public DbService(ApplicationDbContext dbContext, HashPasswordService hashPasswordService) {
            _dbContext = dbContext;
            _hashPasswordService = hashPasswordService;
        }
        public async Task<User?> FindUserByEmail([FromBody] DTOs.LoginRequest loginRequest)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == loginRequest.Email);
        }
        public async Task<bool> AddUserToDatabase([FromBody] DTOs.RegisterRequest registerRequest)
        {
            var isDuplicatedEmail = await CheckDuplicateEmail(registerRequest);
            if (isDuplicatedEmail) {
                return false;
            }
            else
            {
                var user = new User
                {
                    Email = registerRequest.Email,
                    Password = _hashPasswordService.HashPassword(registerRequest.Password),
                    Name = registerRequest.Name,
                };
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();
            }
            return true;
        }
        public async Task<bool> CheckDuplicateEmail([FromBody] DTOs.RegisterRequest registerRequest)
        {   
            if (await _dbContext.Users.AnyAsync(u => u.Email == registerRequest.Email))
            {
                return true;
            }
            return false;
        }
    }
}
