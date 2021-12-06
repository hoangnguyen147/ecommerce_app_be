using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace EcommerceApp.Models
{

    [Table("Category")]
    public class Category
    {
        [Key] public long id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string image { get; set; }
    }
}