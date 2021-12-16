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
    public class OrderController : ControllerBase
    {
        private OrderService _orderService;
        private readonly IHttpContextAccessor _contextAccessor;

        public OrderController(OrderService orderService, IHttpContextAccessor contextAccessor)
        {
            this._orderService = orderService;
            this._contextAccessor = contextAccessor;
        }
        [Route("add-order")]
        [HttpPost]
        public IActionResult AddNewOrder(OrderRequest data)
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                CurrentUser user = SystemAuthorizationService.GetCurrentUser(this._contextAccessor);
                Order newOrder = this._orderService.order(data, user.id);
                responseAPI.Data = newOrder;
                responseAPI.Message = "Thêm đơn hàng thành công";
                responseAPI.Status = 200;
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        
        [Route("get-all-order")]
        [HttpGet]
        public IActionResult GetAllOrder()
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                CurrentUser user = SystemAuthorizationService.GetCurrentUser(this._contextAccessor);
                List<Order> list = this._orderService.getAllOrder(user.role);
                responseAPI.Data = list;
                responseAPI.Message = "Lấy danh sách đơn hàng thành công";
                responseAPI.Status = 200;
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        
        [Route("get-my-order")]
        [HttpGet]
        public IActionResult GetMyOrder()
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                CurrentUser user = SystemAuthorizationService.GetCurrentUser(this._contextAccessor);
                List<Order> list = this._orderService.getMyOrder(user.id);
                responseAPI.Data = list;
                responseAPI.Message = "Lấy danh sách đơn hàng thành công";
                responseAPI.Status = 200;
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        
        [Route("change-status/{order_id}")]
        [HttpPatch]
        public IActionResult ChangeOrderStatus(string status, long order_id)
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                CurrentUser user = SystemAuthorizationService.GetCurrentUser(this._contextAccessor);
                Order item = this._orderService.changeOrderStatus(status, order_id, user.role);
                responseAPI.Data = item;
                responseAPI.Message = "Cập nhật trạng thái đơn hàng thành công";
                responseAPI.Status = 200;
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
        
        [Route("user-cancel-order/{order_id}")]
        [HttpPatch]
        public IActionResult userCancelOrder(long order_id)
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                CurrentUser user = SystemAuthorizationService.GetCurrentUser(this._contextAccessor);
                Order item = this._orderService.userCancelOrder(order_id, user.id);
                responseAPI.Data = item;
                responseAPI.Message = "Cập nhật trạng thái đơn hàng thành công";
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
