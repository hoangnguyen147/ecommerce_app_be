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
    public class CategoryService
    {
        protected readonly AppContext context;

        public CategoryService(AppContext context)
        {
            this.context = context;
        }

        public Category addCategory(AddCategoryRequest category)
        {
            Category item = new Category()
            {
                name = category.name,
                description = category.description,
                image = category.image
            };

            this.context.Categories.Add(item);

            this.context.SaveChanges();

            return item;

        }

        public List<Category> getAllCategory()
        {
            List<Category> _listCategory = context.Categories.ToList();
            
            return _listCategory;
        }


    }
}