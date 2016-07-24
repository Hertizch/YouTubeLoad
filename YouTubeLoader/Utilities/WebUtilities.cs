using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace YouTubeLoader.Utilities
{
    public class WebUtilities
    {
        public static async Task<HttpStatusCode> GetResponseStatusCode(string url)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(new Uri(url)))
                {
                    return response.StatusCode;
                }
            }
        }
    }
}
