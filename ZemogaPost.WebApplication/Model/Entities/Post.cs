using System;
using System.Collections.Generic;
using System.Text;
using ZemogaPost.Domain.Common;

namespace ZemogaPost.WebApplication.Model.Entities
{
    public class Post: BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public bool Approved { get; set; } = false;
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string LastModifiedBy { get; set; }
        public DateTime? ApprovalDate { get; set; }
        //public int UserId { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
