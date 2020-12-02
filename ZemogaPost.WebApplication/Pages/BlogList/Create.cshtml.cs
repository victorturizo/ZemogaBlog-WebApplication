using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using ZemogaPost.WebApplication.Model;
using ZemogaPost.WebApplication.Model.Entities;
using ZemogaPost.WebApplication.Provider;

namespace ZemogaPost.WebApplication.Pages.BlogList
{
    public class CreateModel : PageModel
    {
        private readonly IOptions<AppSettings> appSettings;
        private readonly Executor executor;
        private static HttpClient client = new HttpClient();
        const string SessionUserName = "UserName";
        const string SessionRole = "Role";

        public CreateModel(IOptions<AppSettings> app)
        {
            this.appSettings = app;
            this.executor = new Executor(this.appSettings);
        }

        [BindProperty]
        public Post Post { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            try
            {
                Post.CreatedDate = DateTime.Now;
                Post.CreatedBy = HttpContext.Session.GetString(SessionUserName);
                Post.Approved = false;
                Post.LastModifiedBy = HttpContext.Session.GetString(SessionUserName);
                Post.UserId = 1;
                var response = await client.PostAsJsonAsync("https://localhost:44327/api/BlogPost/SavePost", Post);

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
