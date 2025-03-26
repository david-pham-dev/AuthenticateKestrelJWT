using AuthenticateAppBackEnd.DTOs;
using AuthenticateAppBackEnd.Model.Entity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace AuthenticateAppBackEnd.Services
{
    public class JwtService

    {   private readonly IConfiguration _configuration;
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly DbService _dbService;
        private readonly HashPasswordService _hashPasswordService;

        public JwtService(IConfiguration configuration, DbService dbService, HashPasswordService hashPasswordService)
        {   
            _configuration = configuration;
            _secretKey = configuration["JwtSettings:SecretKey"];
            _issuer = configuration["JwtSettings:Issuer"];
            _audience = configuration["JwtSettings:Audience"];
            _hashPasswordService = hashPasswordService;
            _dbService = dbService;
        }
        public (string Token, int ExpiresIn) GenerateToken(User user)
        {
           var tokenValidityMin = _configuration.GetValue<int>("JwtSettings:TokenValidityMin");
            var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMin);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email.ToString()),
                    ]),
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = credentials,
                Issuer = _issuer,
                Audience = _audience
            };
            var handler = new JsonWebTokenHandler();
            string token = handler.CreateToken(tokenDescriptor);
            return (token, tokenValidityMin * 60);
        }
        public async Task<LoginResponseModel>? Authenticate(DTOs.LoginRequest loginRequest)
        {
            if (string.IsNullOrWhiteSpace(loginRequest.Email) || string.IsNullOrWhiteSpace(loginRequest.Password))
                return null;
            var foundUser = await _dbService.FindUserByEmail(loginRequest);
            if (foundUser == null || !_hashPasswordService.MatchedHashedPassword(loginRequest.Password, foundUser.Password))
            {
                return null;
            }

            var (token, expiresIn) = GenerateToken(foundUser);
            return new LoginResponseModel
            {
                AccessToken = token,
                Email = loginRequest.Email,
                ExpiresIn = expiresIn,
            };
        }
    }

}
