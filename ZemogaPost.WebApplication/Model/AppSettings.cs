using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZemogaPost.WebApplication.Model
{
    public class AppSettings
    {
        private string apiBlog;
        public string APIBlog
        {
            get { return apiBlog; }
            set { this.apiBlog = value; }
        }
    }
}
