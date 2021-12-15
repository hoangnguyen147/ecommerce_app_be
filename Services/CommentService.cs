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

        public Comment addNewComment(AddCommentRequest data, string userId)
        {
            Product product = context.Products.FirstOrDefault(x => x.id == data.product_id);
            if (product == null)
            {
                throw new ArgumentException("Không tìm thấy sản phẩm");
            }

            int count = product.count_vote;
            float newVote = ((product.vote * count) + data.vote) / (count + 1);
            product.vote = newVote;
            System.Diagnostics.Debug.WriteLine(newVote);
            product.count_vote = count + 1;
            DateTime dateNow = DateTime.Now;
            
            Comment item = new Comment()
            {
                content = data.content,
                product_id = data.product_id,
                user_id = Convert.ToInt64(userId),
                vote = data.vote,
                created_at = dateNow,
            };
            
            this.context.Comments.Add(item);

            this.context.SaveChanges();

            return item;

        }

        public List<CommentDto> getAllCommentOfProduct(long product_id)
        {
            List<CommentDto> _listComment = context.Comments
                .Join(context.Users,
                    c => c.user_id,
                    u => u.id,
                    (c, u) => new {Comments = c, Users = u})
                .Where(x => x.Comments.product_id == product_id)
                .Select(x => new CommentDto()
                {
                    content = x.Comments.content,
                    product_id = x.Comments.product_id,
                    vote = x.Comments.vote,
                    avatar = x.Users.avatar,
                    user_id = x.Users.id,
                    fullname = x.Users.fullname,
                    username = x.Users.username,
                }).ToList();

            // var comments = from c in context.Comments
            //     join u in context.Users
            //         on c.user_id equals u.id
            //     select new
            //     {
            //         fullname = u.fullname,
            //         username = u.username,
            //         content = c.content,
            //
            //     };


            return _listComment;
        }


    }
}