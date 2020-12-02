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
    public class IndexModel : PageModel
    {
        private readonly IOptions<AppSettings> appSettings;
        private readonly Executor executor;
        private static HttpClient client = new HttpClient();
        const string SessionUserName = "UserName";
        const string SessionRole = "Role";
        public IndexModel(IOptions<AppSettings> app)
        {
            this.appSettings = app;
            this.executor = new Executor(this.appSettings);
        }

        [BindProperty]
        public IEnumerable<Post>  Posts { get; set; }

        [BindProperty]
        public Post Post { get; set; }

        [BindProperty]
        public User userValidator { get; set; }
        public async Task OnGet(User user)
        {
            try
            {
              
                userValidator = new User()
                {
                    Username = HttpContext.Session.GetString(SessionUserName),
                    Profile = HttpContext.Session.GetString(SessionRole)
                };
                var response =  await client.GetAsync("https://localhost:44327/api/BlogPost/GetAllPost");

                if (response.IsSuccessStatusCode)
                {
                    Posts = JsonConvert.DeserializeObject<IEnumerable<Post>>(await response.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task OnPost(Post Post)
        {
            try
            {

                userValidator = new User()
                {
                    Username = HttpContext.Session.GetString(SessionUserName),
                    Profile = HttpContext.Session.GetString(SessionRole)
                };
                var response = await client.PostAsJsonAsync("https://localhost:44327/api/BlogPost/DeletePost", Post.Id);

                if (response.IsSuccessStatusCode)
                {
                    Page();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
