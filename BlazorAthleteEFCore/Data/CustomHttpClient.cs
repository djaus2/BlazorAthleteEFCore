using Newtonsoft.Json;
using System;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAthleteEFCore.Data
{
    public class CustomHttpClient<T> : HttpClient
    {
        public static string BaseUrl { get; set; }
        private static string TName {get;set;}
        //private string TargetUrl {  get
        //    {
        //        return BaseUrl + $"/api/{TName}/";
        //    } }
        public CustomHttpClient()
        {
            TName = typeof(T).Name;
        }
        public async Task<T> GetJsonAsync(string requestUri)
        {
            HttpClient httpClient = new HttpClient();
            var httpContent = await httpClient.GetAsync(requestUri);
            string jsonContent = httpContent.Content.ReadAsStringAsync().Result;
            T obj = JsonConvert.DeserializeObject<T>(jsonContent);
            httpContent.Dispose();
            httpClient.Dispose();
            return obj;
        }
        public async Task<HttpResponseMessage> PostJsonAsync(string requestUri, T content)
        {
            //"/api/Athletes/create"
            HttpClient httpClient = new HttpClient();
            string myContent = JsonConvert.SerializeObject(content);
            StringContent stringContent = new StringContent(myContent, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(requestUri, stringContent);
            httpClient.Dispose();
            return response;
        }
        public async Task<HttpResponseMessage> PutJsonAsync(string requestUri,T content)
        {
            HttpClient httpClient = new HttpClient();
            string myContent = JsonConvert.SerializeObject(content);
            StringContent stringContent = new StringContent(myContent, Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync(requestUri, stringContent);
            httpClient.Dispose();
            return response;
        }
    }
}
