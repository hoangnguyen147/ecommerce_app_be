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
    public class CommentController : ControllerBase
    {
        private CommentService _commentService;
        private readonly IHttpContextAccessor _contextAccessor;

        public CommentController(CommentService commentService, IHttpContextAccessor contextAccessor)
        {
            this._commentService = commentService;
            this._contextAccessor = contextAccessor;
        }
        [Route("add-comment")]
        [HttpPost]
        public IActionResult AddNewComment(AddCommentRequest data)
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                CurrentUser user = SystemAuthorizationService.GetCurrentUser(this._contextAccessor);
                Comment item = this._commentService.addNewComment(data, user.id);
                responseAPI.Data = item;
                responseAPI.Message = "Thêm bình luận thành công";
                responseAPI.Status = 200;
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        
        [Route("get-comment/{product_id}")]
        [HttpGet]
        public IActionResult GetComments(long product_id)
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                CurrentUser user = SystemAuthorizationService.GetCurrentUser(this._contextAccessor);
                List<CommentDto> list = this._commentService.getAllCommentOfProduct(product_id);
                responseAPI.Data = list;
                responseAPI.Message = "Lấy danh sách bình luận thành công";
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
