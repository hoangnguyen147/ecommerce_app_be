using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApp.DTOs;
using EcommerceApp.Models;
using EcommerceApp.Services;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserController(UserService userService, IHttpContextAccessor contextAccessor)
        {
            this._userService = userService;
            this._contextAccessor = contextAccessor;
        }
        [Route("get-me")]
        [HttpGet]
        public IActionResult GetMe()
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                CurrentUser user = SystemAuthorizationService.GetCurrentUser(this._contextAccessor);
                UserDto info = this._userService.getMe(user.id);
                responseAPI.Data = info;
                responseAPI.Message = "Lấy thông tin cá nhân thành công";
                responseAPI.Status = 200;
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        
        [Route("get-all-user")]
        [HttpGet]
        public IActionResult GetAllUser()
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                CurrentUser user = SystemAuthorizationService.GetCurrentUser(this._contextAccessor);
                List<UserDto> list = this._userService.getListUser(user.role);
                responseAPI.Data = list;
                responseAPI.Message = "Lấy danh sách người dùng thành công";
                responseAPI.Status = 200;
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        
        [Route("update-profile")]
        [HttpPut]
        public IActionResult UpdateProfile(UpdateProfileRequest data)
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                CurrentUser user = SystemAuthorizationService.GetCurrentUser(this._contextAccessor);
                this._userService.UpdateProfile(data, user.id);
                responseAPI.Message = "Cập nhật thông tin thành công";
                responseAPI.Status = 200;
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
       
    }
}
