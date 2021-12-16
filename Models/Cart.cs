using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace EcommerceApp.Models
{

    [Table("Cart")]
    public class Cart
    {
        [Key] public long id { get; set; }
        public long user_id { get; set; }
        public string status { get; set; }
        public DateTime created_at { get; set; }
        [ForeignKey("user_id")]
        public virtual User UserCreate { get; set; }
    }

    
    [Table("CartItem")]
    public class CartItem
    {
        [Key] public long id { get; set; }
        public long cart_id { get; set; }
        public long product_id { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }
        [ForeignKey("cart_id")]
        public virtual Cart Cart { get; set; }
        [ForeignKey("product_id")]
        public virtual Product Product { get; set; }
    }
}