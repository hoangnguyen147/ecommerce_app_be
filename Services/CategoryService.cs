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

        public Category addCategory(AddCategoryRequest category, string userRole)
        {
            if (userRole != "admin")
            {
                throw new ArgumentException("Chỉ dành cho admin");
            }
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
        
        public Category updateCategory(AddCategoryRequest data, long category_id, string userRole)
        {
            if (userRole != "admin")
            {
                throw new ArgumentException("Chỉ dành cho admin");
            }
            Category item = this.context.Categories.FirstOrDefault(x => x.id == category_id);

            if (item == null)
            {
                throw new ArgumentException("Không tìm thấy nhóm sản phẩm");
            }

            item.name = data.name;
            item.description = data.description;
            item.image = data.image;

            context.SaveChanges();

            return item;
        }
        
        public void deleteCategory(long category_id, string userRole)
        {
            if (userRole != "admin")
            {
                throw new ArgumentException("Chỉ dành cho admin");
            }
            Category item = this.context.Categories.FirstOrDefault(x => x.id == category_id);

            if (item == null)
            {
                throw new ArgumentException("Không tìm thấy nhóm sản phẩm");
            }

            context.Categories.Remove(item);
            
            context.SaveChanges();


        }
        
        

    }
}