using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Values
{
    public class LoginResponse
    {
        public string UserID { get; set; }
        public string Username { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
