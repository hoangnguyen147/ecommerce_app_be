using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceApp.DTOs
{
    public class LoginRequest
    {
        public string username { get; set; }
        public string password { get; set; }
    }
    
    public class RegisterRequest
    {
        public string username { get; set; }
        public string password { get; set; }
        public string fullname { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string avatar { get; set; }
    }

    public class CurrentUser
    {
        public string id { get; set; }
        public string role { get; set; }
    }

    public class ChangePasswordRequest
    {
        public string old_password { get; set; }
        public string new_password { get; set; }
    }

    public class UserDto
    {
        public long id { get; set; }
        public string username { get; set; }
        public string fullname { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public int finance { get; set; }
        public string avatar { get; set; }
        public bool is_admin { get; set; }
    }

    public class UpdateProfileRequest
    {
        public string fullname { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string avatar { get; set; }
    }
}