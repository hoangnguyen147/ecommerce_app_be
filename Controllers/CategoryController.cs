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
    public class CategoryController : ControllerBase
    {
        private CategoryService _categoryService;
        private readonly IHttpContextAccessor _contextAccessor;

        public CategoryController(CategoryService categoryService, IHttpContextAccessor contextAccessor)
        {
            this._categoryService = categoryService;
            this._contextAccessor = contextAccessor;
        }
        [Route("add-category")]
        [HttpPost]
        public IActionResult AddNewCategory(AddCategoryRequest data)
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                CurrentUser user = SystemAuthorizationService.GetCurrentUser(this._contextAccessor);
                Category item = this._categoryService.addCategory(data, user.role);
                responseAPI.Data = item;
                responseAPI.Message = "Thêm nhóm sản phẩm thành công";
                responseAPI.Status = 200;
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        
        [Route("")]
        [HttpGet]
        public IActionResult GetAllCategory()
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                CurrentUser user = SystemAuthorizationService.GetCurrentUser(this._contextAccessor);
                List<Category> list = this._categoryService.getAllCategory();
                responseAPI.Data = list;
                responseAPI.Message = "Lấy danh sách nhóm sản phẩm thành công";
                responseAPI.Status = 200;
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        
        [Route("update-category/{category_id}")]
        [HttpPut]
        public IActionResult updateProduct(AddCategoryRequest data, long category_id)
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                CurrentUser user = SystemAuthorizationService.GetCurrentUser(this._contextAccessor);
                Category item = this._categoryService.updateCategory(data, category_id, user.role);
                responseAPI.Data = item;
                responseAPI.Message = "Sửa nhóm sản phẩm thành công";
                responseAPI.Status = 200;
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        
        
        [Route("{category_id}")]
        [HttpDelete]
        public IActionResult deleteProduct(long category_id)
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                CurrentUser user = SystemAuthorizationService.GetCurrentUser(this._contextAccessor);
                this._categoryService.deleteCategory(category_id, user.role);
                responseAPI.Message = "Xóa nhóm sản phẩm thành công";
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
