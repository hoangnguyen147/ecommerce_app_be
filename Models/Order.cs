using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace EcommerceApp.Models
{

    [Table("Orders")]
    public class Order
    {
        [Key] public long id { get; set; }
        public long user_id { get; set; }
        public string status { get; set; }
        public DateTime created_at { get; set; }
        public string message { get; set; }
        public virtual ICollection<OrderItem> Items { get; set; }
        [ForeignKey("user_id")]
        public virtual User UserCreate { get; set; }
    }

    
    [Table("OrderItem")]
    public class OrderItem
    {
        [Key] public long id { get; set; }
        public long order_id { get; set; }
        public long product_id { get; set; }
        public int quantity { get; set; }
        [ForeignKey("order_id")]
        public virtual Order Order { get; set; }
        [ForeignKey("product_id")]
        public virtual Product Product { get; set; }
    }
}