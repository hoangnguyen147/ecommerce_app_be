using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcommerceApp.Models
{
    public class AppContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }

        public AppContext(DbContextOptions<AppContext> options) : base(options) { }
        

    }
}