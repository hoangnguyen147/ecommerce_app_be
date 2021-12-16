using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceApp.DTOs
{
    public class OrderRequest
    {
        public string message { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }

    public class OrderItemDto
    {
        public long product_id { get; set; }
        public int quantity { get; set; }
    }

    public class OrderStatus
    {
        public string pending = "pending";
        public string delivering = "delivering";
        public string success = "success";
        public string reject = "reject";
        public string cancel = "cancel";

    }


}