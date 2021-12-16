using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EcommerceApp.Models;
using EcommerceApp.Utils;
using EcommerceApp.DTOs;
using AppContext = EcommerceApp.Models.AppContext;

namespace EcommerceApp.Services
{
    public class AuthService
    {
        protected readonly AppContext context;

        public AuthService(AppContext context)
        {
            this.context = context;
        }

        public AccessToken Login(LoginRequest user, bool forAdmin)
        {
            string passCheck = DataHelper.SHA256Hash(user.username + "_" + user.password);

            User userExist = context.Users.Where(x => x.username.Equals(user.username) && x.password.Equals(passCheck)).FirstOrDefault();
            if (userExist == null)
            {
                throw new ArgumentException("Sai thông tin đăng nhập");
            }

            if (forAdmin && !userExist.is_admin)
            {
                throw new ArgumentException("Khong co quyen truy cap");
            }

            string role;

            if (userExist.is_admin == false)
            {
                role = "user";
            }
            else
            {
                role = "admin";
            }
            
            context.SaveChanges();

            DateTime expirationDate = DateTime.Now.Date.AddMinutes(EnviConfig.ExpirationInMinutes);
            long expiresAt = (long)(expirationDate - new DateTime(1970, 1, 1)).TotalSeconds;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(EnviConfig.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
              {
                        new Claim(ClaimTypes.Sid, userExist.id.ToString()),
                        new Claim(ClaimTypes.Role, role),
                        new Claim(ClaimTypes.Name, userExist.username),
                        new Claim(ClaimTypes.Expiration, expiresAt.ToString())
              }),
                Expires = expirationDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new AccessToken
            {
                user = userExist.username,
                fullname = userExist.fullname,
                avatar = userExist.avatar,
                token = tokenHandler.WriteToken(token),
            };
        }

        public void SignUp(RegisterRequest user)
        {
            if (context.Users.Any(x => x.username.Equals(user.username)))
                throw new ArgumentException("Tài khoản đã tồn tại");

            User newUser = new User()
            {
                username = user.username,
                fullname = user.fullname,
                email = user.email,
                phone = user.phone,
                password = DataHelper.SHA256Hash(user.username + "_" + user.password),
                avatar = user.avatar,
                address = user.address,
                finance = 0,
            };

            context.Users.Add(newUser);
            context.SaveChanges();
        }

        public void changePassword(ChangePasswordRequest data, string user_id)
        {
            User user = context.Users.FirstOrDefault(x => x.id.ToString() == user_id);
            if (user == null)
            {
                throw new ArgumentException("Tài khoản không tồn tại");
            }
            string passCheck = DataHelper.SHA256Hash(user.username + "_" + data.old_password);

            if (passCheck != user.password)
            {
                throw new ArgumentException("Sai mật khẩu");
            }
            
            string newPassword = DataHelper.SHA256Hash(user.username + "_" + data.new_password);

            user.password = newPassword;

            context.SaveChanges();
        }
        
        public void adminResetPassword(string reseted_username, string new_password, string role)
        {
            if (role != "admin")
            {
                throw new ArgumentException("Chỉ dành cho admin");
            }
            
            User user = context.Users.FirstOrDefault(x => x.username == reseted_username);
            if (user == null)
            {
                throw new ArgumentException("Tài khoản không tồn tại");
            }
            
            string newPassword = DataHelper.SHA256Hash(user.username + "_" + new_password);

            user.password = newPassword;

            context.SaveChanges();
        }
        
    }
}