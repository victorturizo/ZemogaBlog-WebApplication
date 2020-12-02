using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZemogaPost.WebApplication.Model
{
    public class ResponseDTO<T>
    {
        public T Data { get; set; }
    }
}
