using System;
using System.Collections.Generic;
using System.Text;
using ZemogaPost.Domain.Common;

namespace ZemogaPost.WebApplication.Model.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Profile { get; set; }


        public ICollection<Post> Posts { get; set; }
    }
}
