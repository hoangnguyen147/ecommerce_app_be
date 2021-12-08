using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceApp.DTOs
{
    public class AddCategoryRequest
    {
        public string name { get; set; }
        public string description { get; set; }
        public string image { get; set; }
    }
}