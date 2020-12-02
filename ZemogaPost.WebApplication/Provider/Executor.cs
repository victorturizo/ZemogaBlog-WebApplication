using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ZemogaPost.WebApplication.Model;

namespace ZemogaPost.WebApplication.Provider
{
    public class Executor
    {
        private static HttpClient client = new HttpClient();

        public Executor(IOptions<AppSettings> app)
        {

        }

        public async Task<T> ExecuteAsync<T>(string api, T data)
        {
            try
            {
                var tries = 1;
                while (tries <= 2)
                {
                    var response = await client.PostAsJsonAsync(api, data);

                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsAsync<T>();
                    }

                    tries += 1;
                }

                return default;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + ": " + e.InnerException);
            }
        }

        public async Task<T> ExecuteAsync<T>(string api)
        {
            try
            {
                var tries = 1;
                while (tries <= 2)
                {
                    var response = await client.GetAsync("https://localhost:44327/api/BlogPost/GetAllPost");

                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsAsync<T>();
                    }

                    tries += 1;
                }

                return default;
            }
            catch (Exception e)
            {
                return default;
            }
        }
    }
}
