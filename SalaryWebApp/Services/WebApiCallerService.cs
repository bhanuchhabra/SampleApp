using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace SalaryWebApp.Services
{
    public class WebApiCallerService
    {
        public async Task<string> WebApiCaller(Uri baseUrl,string url)
        {
            string result = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress =baseUrl;

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage apiResponse = await client.GetAsync(url);
                if (apiResponse.IsSuccessStatusCode)
                {
                    result = await apiResponse.Content.ReadAsStringAsync();

                }
            }
            return result;
        }

    }
}