using System;
using System.Collections.Generic;
using System.Text;
using ZemogaPost.Domain.Common;

namespace ZemogaPost.WebApplication.Model.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        

        public int PostId { get; set; }
    }
}
