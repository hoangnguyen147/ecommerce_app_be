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
    public class AuthController : ControllerBase
    {
        private AuthService _authService;
        private IWebHostEnvironment _hostEnvironment;
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthController(AuthService authService, IWebHostEnvironment hostEnvironment, IHttpContextAccessor contextAccessor)
        {
            this._authService = authService;
            this._hostEnvironment = hostEnvironment;
            this._contextAccessor = contextAccessor;
        }
        [Route("login")]
        [HttpPost]
        public IActionResult Login(LoginRequest user)
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                AccessToken accessToken = this._authService.Login(user, false);
                responseAPI.Data = accessToken;
                responseAPI.Status = 200;
                responseAPI.Message = "Đăng nhập thành công";
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        
        [Route("admin-login")]
        [HttpPost]
        public IActionResult AdminLogin(LoginRequest user)
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                AccessToken accessToken = this._authService.Login(user, true);
                responseAPI.Data = accessToken;
                responseAPI.Status = 200;
                responseAPI.Message = "Đăng nhập thành công";
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        
        [Route("sign-up")]
        [HttpPost]
        public IActionResult SignUp(RegisterRequest user)
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                this._authService.SignUp(user);
                responseAPI.Status = 200;
                responseAPI.Message = "Đăng ký thành công";
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        
        [Route("change-password")]
        [HttpPatch]
        public IActionResult UserChangePassword(ChangePasswordRequest data)
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                CurrentUser user = SystemAuthorizationService.GetCurrentUser(this._contextAccessor);
                this._authService.changePassword(data, user.id);
                responseAPI.Status = 200;
                responseAPI.Message = "Đổi mật khẩu thành công";
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        
        [Route("admin-reset-password")]
        [HttpPatch]
        public IActionResult AdminResetPassword(string username, string new_password)
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                CurrentUser user = SystemAuthorizationService.GetCurrentUser(this._contextAccessor);
                this._authService.adminResetPassword(username, new_password, user.role);
                responseAPI.Status = 200;
                responseAPI.Message = "Đặt lại mật khẩu thành công";
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
