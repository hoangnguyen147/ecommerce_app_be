using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace EcommerceApp.Models
{

    [Table("User")]
    public class User
    {
        [Key] public long id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string fullname { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public int finance { get; set; }
        public string avatar { get; set; }
        public bool is_admin { get; set; }
        
    }
    
    
}