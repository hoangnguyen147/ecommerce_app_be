using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EcommerceApp.Models;
using EcommerceApp.Utils;
using EcommerceApp.DTOs;
using Microsoft.EntityFrameworkCore;
using AppContext = EcommerceApp.Models.AppContext;

namespace EcommerceApp.Services
{
    public class CommentService
    {
        protected readonly AppContext context;

        public CommentService(AppContext context)
        {
            this.context = context;
        }

        public Comment addComment(AddCommentRequest data, string userId)
        {
            Product product = context.Products.FirstOrDefault(x => x.id == data.product_id);
            Comment item = new Comment()
            {
                content = data.content,
                product_id = data.product_id,
                user_id = Convert.ToInt64(userId),
                vote = data.vote
            };

            return item;

        }

        public List<Product> getAllProduct()
        {
            List<Product> _listProduct = context.Products.ToList();
            
            this.context.Database.Migrate();
            
            return _listProduct;
        }

        public List<Product> getProductByCategory(long category_id)
        {
            List<Product> _listProduct = context.Products
                .Where(x => x.category_id.Equals(category_id))
                .Select(x => x).ToList();

            return _listProduct;
        }

        public Product updateProduct(AddProductRequest product, long product_id, string userRole)
        {
            if (userRole != "admin")
            {
                throw new ArgumentException("Lỗi xác thực");
            }
            Product item = this.context.Products.FirstOrDefault(x => x.id == product_id);

            if (item == null)
            {
                throw new ArgumentException("Không tìm thấy sản phẩm");
            }

            item.category_id = product.category_id;
            item.name = product.name;
            item.price = product.price;
            item.quantity = product.quantity;
            item.vote = product.vote;
            item.image = product.image;

            this.context.SaveChanges();
            
            return item;
        }

        public Product setProductHot(long id, string userRole)
        {
            if (userRole != "admin")
            {
                throw new ArgumentException("Lỗi xác thực");
            }
            Product item = this.context.Products.FirstOrDefault(x => x.id == id);

            if (item == null)
            {
                throw new ArgumentException("Không tìm thấy sản phẩm");
            }

            item.is_hot = !item.is_hot;

            this.context.SaveChanges();

            return item;
        }
        
        public void deleteProduct(long id, string userRole)
        {
            if (userRole != "admin")
            {
                throw new ArgumentException("Lỗi xác thực");
            }
            Product item = this.context.Products.FirstOrDefault(x => x.id == id);

            if (item == null)
            {
                throw new ArgumentException("Không tìm thấy sản phẩm");
            }

            context.Products.Remove(item);
            
            this.context.SaveChanges();

        }


    }
}