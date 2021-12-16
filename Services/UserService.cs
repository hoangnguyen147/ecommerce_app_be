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
using Microsoft.EntityFrameworkCore;
using AppContext = EcommerceApp.Models.AppContext;

namespace EcommerceApp.Services
{
    public class UserService
    {
        protected readonly AppContext context;
        

        public UserService(AppContext context)
        {
            this.context = context;
        }

        public UserDto getMe(string user_id)
        {
            User user = context.Users.FirstOrDefault(x => x.id.ToString() == user_id);

            if (user == null)
            {
                throw new ArgumentException("Tài khoản không tồn tại");
            }

            UserDto info = new UserDto()
            {
                id = user.id,
                username = user.username,
                fullname = user.fullname,
                address = user.address,
                avatar = user.avatar,
                email = user.email,
                finance = user.finance,
                phone = user.phone,
                is_admin = user.is_admin
            };

            return info;
        }

        public List<UserDto> getListUser(string userRole)
        {
            if (userRole != "admin")
            {
                throw new ArgumentException("Chỉ dành cho admin");
            }

            List<UserDto> list = context.Users.Select(user => new UserDto()
            {
                id = user.id,
                username = user.username,
                fullname = user.fullname,
                address = user.address,
                avatar = user.avatar,
                email = user.email,
                finance = user.finance,
                phone = user.phone,
                is_admin = user.is_admin
            }).ToList();

            return list;

        }

        public void UpdateProfile(UpdateProfileRequest data, string user_id)
        {
            User user = context.Users.FirstOrDefault(x => x.id.ToString() == user_id);

            if (user == null)
            {
                throw new ArgumentException("Tài khoản không tồn tại");
            }

            user.fullname = data.fullname;
            user.email = data.email;
            user.avatar = data.avatar;
            user.address = data.address;
            user.phone = data.phone;

            context.SaveChanges();

        }
        
       


    }
}