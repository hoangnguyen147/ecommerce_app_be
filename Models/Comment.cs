using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace EcommerceApp.Models
{

    [Table("Comment")]
    public class Comment
    {
        [Key] public long id { get; set; }
        public string content { get; set; }
        public long product_id { get; set; }
        public long user_id { get; set; }
        public float vote { get; set; }
        public DateTime created_at { get; set; }
        [ForeignKey("user_id")]
        public virtual User UserCreate { get; set; }
        [ForeignKey("product_id")]
        public virtual Product Product { get; set; }
    }
}