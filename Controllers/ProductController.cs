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
    public class ProductController : ControllerBase
    {
        private ProductService _productService;
        private readonly IHttpContextAccessor _contextAccessor;

        public ProductController(ProductService productService, IHttpContextAccessor contextAccessor)
        {
            this._productService = productService;
            this._contextAccessor = contextAccessor;
        }
        [Route("add-product")]
        [HttpPost]
        public IActionResult AddNewProduct(AddProductRequest data)
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                CurrentUser user = SystemAuthorizationService.GetCurrentUser(this._contextAccessor);
                Product item = this._productService.addProduct(data, user.role);
                responseAPI.Data = item;
                responseAPI.Message = "Thêm sản phẩm mới thành công";
                responseAPI.Status = 200;
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        
        [Route("get-all-product")]
        [HttpGet]
        public IActionResult GetAllProduct()
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                CurrentUser user = SystemAuthorizationService.GetCurrentUser(this._contextAccessor);
                List<Product> list = this._productService.getAllProduct();
                responseAPI.Data = list;
                responseAPI.Message = "Lấy danh sách sản phẩm thành công";
                responseAPI.Status = 200;
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        
        [Route("get-by-category/{category_id}")]
        [HttpGet]
        public IActionResult GetProductByCategory(long category_id)
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                CurrentUser user = SystemAuthorizationService.GetCurrentUser(this._contextAccessor);
                List<Product> list = this._productService.getProductByCategory(category_id);
                responseAPI.Data = list;
                responseAPI.Status = 200;
                responseAPI.Message = "Lấy danh sách sản phẩm theo nhóm thành công";
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        
        [Route("update-product/{product_id}")]
        [HttpPut]
        public IActionResult updateProduct(AddProductRequest data, long product_id)
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                CurrentUser user = SystemAuthorizationService.GetCurrentUser(this._contextAccessor);
                Product item = this._productService.updateProduct(data, product_id, user.role);
                responseAPI.Data = item;
                responseAPI.Status = 200;
                responseAPI.Message = "Cập nhật sản phẩm thành công";
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        
        [Route("set-hot/{product_id}")]
        [HttpPatch]
        public IActionResult setProductIsHot(long product_id)
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                CurrentUser user = SystemAuthorizationService.GetCurrentUser(this._contextAccessor);
                Product item = this._productService.setProductHot(product_id, user.role);
                responseAPI.Data = item;
                responseAPI.Status = 200;
                responseAPI.Message = "Cập nhật sản phẩm vào danh sách đang hot thành công";
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        
        [Route("{product_id}")]
        [HttpDelete]
        public IActionResult deleteProduct(long product_id)
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                CurrentUser user = SystemAuthorizationService.GetCurrentUser(this._contextAccessor);
                this._productService.deleteProduct(product_id, user.role);
                responseAPI.Message = "Xóa sản phẩm thành công";
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
