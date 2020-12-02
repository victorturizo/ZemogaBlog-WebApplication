using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ZemogaPost.WebApplication.Model;
using ZemogaPost.WebApplication.Model.Entities;
using ZemogaPost.WebApplication.Provider;

namespace ZemogaPost.WebApplication.Pages.BlogList
{
    public class EditModel : PageModel
    {
        private readonly IOptions<AppSettings> appSettings;
        private readonly Executor executor;
        private static HttpClient client = new HttpClient();
        const string SessionUserName = "UserName";
        const string SessionRole = "Role";
        public EditModel(IOptions<AppSettings> app)
        {
            this.appSettings = app;
            this.executor = new Executor(this.appSettings);
        }

        [BindProperty]
        public Post Post { get; set; }
        public static Post PostApi { get; set; }

        [BindProperty]
        public User userValidator { get; set; }
        public async Task OnGet(int Id)
        {
            try
            {
                userValidator = new User()
                {
                    Username = HttpContext.Session.GetString(SessionUserName),
                    Profile = HttpContext.Session.GetString(SessionRole)
                };
                client.BaseAddress = new Uri("https://localhost:44327/api/");
                var response = await client.PostAsJsonAsync("https://localhost:44327/api/BlogPost/GetPostById", Id);

                if (response.IsSuccessStatusCode)
                {
                    Post = JsonConvert.DeserializeObject<Post>(await response.Content.ReadAsStringAsync());
                    PostApi = Post;

                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public async Task<IActionResult> OnPost(int Id)
        {
            try
            {
                Post.Id = Id;
                Post.Title = Post.Title;
                Post.Content = Post.Content;
                Post.Approved = Post.Approved;
                Post.LastModifiedBy = "vhturizo";
                Post.ApprovalDate = Post.Approved != false ? DateTime.Now : Post.ApprovalDate;
                Post.UserId = PostApi.UserId;
                Post.CreatedDate = Post.CreatedDate;
                Post.Comments = Post.Comments;
              
                var response = await client.PostAsJsonAsync("https://localhost:44327/api/BlogPost/UpdatePost", Post);

                if (response.IsSuccessStatusCode)
                {
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
