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
    public class ProductService
    {
        protected readonly AppContext context;

        public ProductService(AppContext context)
        {
            this.context = context;
        }

        public Product addProduct(AddProductRequest product, string userRole)
        {
            if (userRole != "admin")
            {
                throw new ArgumentException("Lỗi xác thực");
            }
            Product item = new Product()
            {
                category_id = product.category_id,
                name = product.name,
                image = product.image,
                price = product.price,
                quantity = product.quantity,
                vote = 0,
                overview = product.overview,
                is_hot = false
            };

            this.context.Products.Add(item);

            this.context.SaveChanges();

            return item;

        }

        public List<Product> getAllProduct()
        {
            List<Product> _listProduct = context.Products.ToList();
            
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