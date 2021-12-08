using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace EcommerceApp.Models
{

    [Table("Product")]
    public class Product
    {
        [Key] public long id { get; set; }
        public long category_id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }
        public float vote { get; set; }
        public bool is_hot { get; set; }
        
        [ForeignKey("category_id")]
        public virtual Category Category { get; set; }
    }
}