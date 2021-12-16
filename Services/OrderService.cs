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
    public class OrderService
    {
        protected readonly AppContext context;
        
        private OrderStatus _status;

        public OrderService(AppContext context)
        {
            this.context = context;
        }

        public Order order(OrderRequest data, string userId)
        {
            DateTime dateNow = DateTime.Now;
            
            Order newOrder = new Order()
            {
                user_id = Convert.ToInt64(userId),
                message = data.message,
                created_at = dateNow,
                status = _status.pending,
            };

            // this.context.Orders.Add(newOrder);
            System.Diagnostics.Debug.WriteLine(newOrder);


            List<OrderItem> listItems = data.Items.Select(x => new OrderItem()
            {
                order_id = newOrder.id,
                product_id = x.product_id,
                quantity = x.quantity,
            }).ToList();

            newOrder.Items = listItems;

            this.context.Orders.Add(newOrder);
            this.context.SaveChanges();

            return newOrder;


        }

        public List<Order> getAllOrder(string userRole)
        {
            if (userRole != "admin")
            {
                throw new ArgumentException("Chỉ dành cho admin");
            }
            List<Order> listOrder = this.context.Orders.ToList();
            
            return listOrder;
        }
        
        
        public List<Order> getMyOrder(string userId)
        {
            long user_id = Convert.ToInt64(userId);
            List<Order> listOrder = this.context.Orders.Where(x => x.user_id == user_id).ToList();
            
            return listOrder;
        }

        public Order changeOrderStatus(string status, long order_id, string userRole)
        {
            if (userRole != "admin")
            {
                throw new ArgumentException("Chỉ dành cho admin");
            }
            Order order = this.context.Orders.FirstOrDefault(x => x.id == order_id);
            if (order == null)
            {
                throw new ArgumentException("Đơn hàng không tồn tại");
            }

            if (status != _status.pending && status != _status.delivering && status != _status.success &&
                status != _status.reject)
            {
                throw new ArgumentException("Tình trạng đơn hàng không hợp lệ");
            }
            
            order.status = status;

            this.context.SaveChanges();
            
            return order;
        }
        
        public Order userCancelOrder(long order_id, string userId)
        {
            Order order = this.context.Orders.FirstOrDefault(x => x.id == order_id);
            if (order == null)
            {
                throw new ArgumentException("Đơn hàng không tồn tại");
            }

            if (order.user_id.ToString() != userId)
            {
                throw new ArgumentException("Bạn không phải là chủ sở hữu của đơn hàng này");
            }
            
            order.status = _status.cancel;

            this.context.SaveChanges();
            
            return order;
        }
       


    }
}