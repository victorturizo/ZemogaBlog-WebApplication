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
    public class CommentsModel : PageModel
    {

        private readonly IOptions<AppSettings> appSettings;
        private readonly Executor executor;
        private static HttpClient client = new HttpClient();
        public CommentsModel(IOptions<AppSettings> app)
        {
            this.appSettings = app;
            this.executor = new Executor(this.appSettings);
        }

        [BindProperty]
        public Post Post { get; set; }

        [BindProperty]
        public Comment Comment { get; set; }
        public static Post PostApi { get; set; }
        public async Task OnGet(int Id)
        {
            client.BaseAddress = new Uri("https://localhost:44327/api/");
            var response = await client.PostAsJsonAsync("https://localhost:44327/api/BlogPost/GetPostById", Id);

            if (response.IsSuccessStatusCode)
            {
                Post = JsonConvert.DeserializeObject<Post>(await response.Content.ReadAsStringAsync());
                PostApi = Post;

            }
        }


        public async Task<IActionResult> OnPost(int Id)
        {
            try
            {
                Comment.Content = Comment.Content;
                Comment.CreatedBy = "vhturizo";
                Comment.CreatedDate = DateTime.Now;
                Comment.PostId = Id;
                
                //Post.Id = Id;
                //Post.Title = Post.Title;
                //Post.Content = Post.Content;
                //Post.Approved = Post.Approved;
                //Post.LastModifiedBy = "vhturizo";
                //Post.ApprovalDate = Post.Approved != false ? DateTime.Now : Post.ApprovalDate;
                //Post.UserId = PostApi.UserId;
                //Post.CreatedDate = Post.CreatedDate;
                //Post.Comments = Post.Comments;

                var response = await client.PostAsJsonAsync("https://localhost:44327/api/Comment/SaveComment", Comment);

                if (response.IsSuccessStatusCode)
                {
                    // Post = JsonConvert.DeserializeObject<Post>(await response.Content.ReadAsStringAsync());
                    return RedirectToPage("Index");
                }
                else
                {
                    return Page();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
