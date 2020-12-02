using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ZemogaPost.WebApplication.Model;
using ZemogaPost.WebApplication.Model.Entities;
using ZemogaPost.WebApplication.Provider;

namespace ZemogaPost.WebApplication.Pages.BlogList
{
    public class ViewMorePostModel : PageModel
    {
        private readonly IOptions<AppSettings> appSettings;
        private readonly Executor executor;
        private static HttpClient client = new HttpClient();
        public ViewMorePostModel(IOptions<AppSettings> app)
        {
            this.appSettings = app;
            this.executor = new Executor(this.appSettings);
        }

        [BindProperty]
        public Post Post { get; set; }

        [BindProperty]
        public List<Comment> Comment { get; set; }

        public static Post PostApi { get; set; }
        public async Task OnGet(int Id)
        {       
      
            using var response = await client.PostAsJsonAsync("https://localhost:44327/api/BlogPost/GetPostById", Id);

            if (response.IsSuccessStatusCode)
            {
                Post = JsonConvert.DeserializeObject<Post>(await response.Content.ReadAsStringAsync());
                PostApi = Post;
            }

            using var responseComments = await client.PostAsJsonAsync("https://localhost:44327/api/Comment/GetCommentByPostId", Id);

            if (responseComments.IsSuccessStatusCode)
            {
                Comment = JsonConvert.DeserializeObject<List<Comment>>(await responseComments.Content.ReadAsStringAsync());
               
            }
        }
    }
}
