using System.Security.Cryptography;
using System.Text;

namespace AuthenticateAppBackEnd.Services
{
    public class HashPasswordService
    {
        public string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
        public bool MatchedHashedPassword(string enteredPassword, string storedPassword) {
            return storedPassword == HashPassword(enteredPassword);
        }
    }
}
