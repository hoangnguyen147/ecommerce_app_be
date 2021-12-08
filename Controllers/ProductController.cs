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
                Product item = this._productService.addProduct(data);
                responseAPI.Data = item;
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
        public IActionResult GetAllProduct()
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                List<Product> list = this._productService.getAllProduct();
                responseAPI.Data = list;
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
                List<Product> list = this._productService.getProductByCategory(category_id);
                responseAPI.Data = list;
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
