using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceApp.DTOs
{
    public class AddProductRequest
    {
        public long category_id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }
        public float vote { get; set; }
    }
}