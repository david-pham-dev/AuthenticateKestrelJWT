using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.Models
{
    public class UserAccountSession
    {
        public string? email { get; set; }
        public string? accessToken { get; set; }
        public int? expiresIn { get; set; }
    }
}
