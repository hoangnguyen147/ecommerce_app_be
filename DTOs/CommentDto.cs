using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceApp.DTOs
{
    public class AddCommentRequest
    {
        public string content { get; set; }
        public long product_id { get; set; }
        public float vote { get; set; }
    }
}