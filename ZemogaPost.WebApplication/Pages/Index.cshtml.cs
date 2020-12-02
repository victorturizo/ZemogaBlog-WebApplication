using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ZemogaPost.WebApplication.Model;
using ZemogaPost.WebApplication.Model.Entities;
using ZemogaPost.WebApplication.Provider;

namespace ZemogaPost.WebApplication.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IOptions<AppSettings> appSettings;
        private readonly Executor executor;
        private static HttpClient client = new HttpClient();
        const string SessionUserName = "UserName";
        const string SessionRole= "Role";
        const string SessionIdRole = "";

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IEnumerable<User> Users { get; set; }

        [BindProperty]
        public User user { get; set; }
        public void OnGet(int Id)
        {
            if (Id == 3)
            {
                Logout();
            }
            else
            {
                HttpContext.Session.SetString(SessionUserName, "Guess");
                HttpContext.Session.SetString(SessionRole, "Guess");
            }
       
        }
  
        public IActionResult OnPost(int Id)
        {
       
            List<User> usersDummy = new List<User>();
            usersDummy.Add(new User()
            {
                Id = 1,
                Email = "VictorTurizo91@hotmail.com",
                Password = "12345",
                Profile = "Admin",
                Username = "vhturizo"
            });
            usersDummy.Add(new User()
            {
                Id = 2,
                Email = "Writer123@hotmail.com",
                Password = "12345",
                Profile = "Writer",
                Username = "Writer123"
            }            
            );

            var findUSer =usersDummy.Where(m => m.Username == user.Username && m.Password == user.Password).FirstOrDefault();
            if (findUSer!=null)
            {
                HttpContext.Session.SetString(SessionUserName, findUSer.Username);
                HttpContext.Session.SetString(SessionRole, findUSer.Profile);
                

                return RedirectToPage("/BlogList/Index", findUSer);
            }
            else
            {
                
                return Page();      
            }
           
        }

        public void Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.SetString(SessionUserName, "Guess");
            HttpContext.Session.SetString(SessionRole, "Guess");
            //user = new User() {
            //    Id = 2,
            //    Email = "Guess@hotmail.com",
            //    Password = "12345",
            //    Profile = "Guess",
            //    Username = "Guess"
            //};

            RedirectToPage("/BlogList/Index", user);
        }
    }
}
