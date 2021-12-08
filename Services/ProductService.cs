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

        public Product addProduct(AddProductRequest product)
        {
            Product item = new Product()
            {
                category_id = product.category_id,
                name = product.name,
                image = product.image,
                price = product.price,
                quantity = product.quantity,
                vote = product.vote
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
                .Select(x => new Product()
                {
                    id = x.id,
                    category_id = x.category_id,
                    name = x.name,
                    price = x.price
                }).ToList();

            return _listProduct;
        }


    }
}