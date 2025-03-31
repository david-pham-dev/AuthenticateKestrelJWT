using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.Models
{
    public class UserAccountSession
    {
        private static UserAccountSession _instance;
        public static UserAccountSession Instance => _instance ??= new UserAccountSession();
        public string? email { get; set; }
        public string? accessToken { get; set; }
        public int? expiresIn { get; set; }
        public bool IsLoggedIn => !string.IsNullOrEmpty(accessToken);
        public void SetSession(string token, string userEmail)
        {
            accessToken = token;
            email = userEmail;
        }

        public void ClearSession()
        {
            accessToken = null;
            email = null;
        }
    }
}
