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

        public AccessToken Login(User user)
        {
            System.Diagnostics.Debug.WriteLine(user.username);
            System.Diagnostics.Debug.WriteLine(user.password);
            string passCheck = DataHelper.SHA256Hash(user.username + "_" + user.password);
            // string passCheck = "5fa02eb08b4a56ab93e1c255076a0de41acb20ebcc3d9421938e9567c66c0266";

            User userExist = context.Users.Where(x => x.username.Equals(user.username) && x.password.Equals(passCheck)).FirstOrDefault();
            if (userExist == null)
            {
                throw new ArgumentException("Sai thông tin đăng nhập");
            }
            context.SaveChanges();

            DateTime expirationDate = DateTime.Now.Date.AddMinutes(EnviConfig.ExpirationInMinutes);
            System.Diagnostics.Debug.WriteLine("alo 2");
            long expiresAt = (long)(expirationDate - new DateTime(1970, 1, 1)).TotalSeconds;
            var tokenHandler = new JwtSecurityTokenHandler();
            System.Diagnostics.Debug.WriteLine("alo 2.5");
            System.Diagnostics.Debug.WriteLine("alo 2.55");
            var key = Encoding.ASCII.GetBytes(EnviConfig.SecretKey);
            System.Diagnostics.Debug.WriteLine("alo 3");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
              {
                        new Claim(ClaimTypes.Name, userExist.username),
                        new Claim(ClaimTypes.Expiration, expiresAt.ToString())
              }),
                Expires = expirationDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            System.Diagnostics.Debug.WriteLine("alo 4");
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new AccessToken
            {
                user = userExist.username,
                fullname = userExist.fullname,
                avatar = userExist.avatar,
                token = tokenHandler.WriteToken(token),
            };
        }

        public void SignUp(User user)
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
                avatar = ""
            };

            context.Users.Add(newUser);
            context.SaveChanges();
        }
        
    }
}