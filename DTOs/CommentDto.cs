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

    public class CommentDto
    {
        public string content { get; set; }
        public long product_id { get; set; }
        public float vote { get; set; }
        public string username { get; set; }
        public string fullname { get; set; }
        public long user_id { get; set; }
        public string avatar { get; set; }
    }
}