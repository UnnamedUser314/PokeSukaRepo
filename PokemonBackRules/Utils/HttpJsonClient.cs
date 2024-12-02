using PokemonBackRules.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PokemonBackRules.Utils
{
    public static class HttpJsonClient<T>
    {
        public static async Task<T?> Get(string url)
        {
            using HttpClient httpClient = new HttpClient();
            {
                HttpResponseMessage datos = await httpClient.GetAsync(url);
                string dataget = await datos.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(dataget);
            }
        }
        public static async Task<List<T?>> GetAll(string url)
        {
            using HttpClient httpClient = new HttpClient();
            {
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<T>>(responseBody);
            }
        }
        public static async void Post(string url, object data)
        {
            using HttpClient httpClient = new HttpClient();
            {

                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    string errorDetails = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {response.StatusCode}");
                    Console.WriteLine($"Details: {errorDetails}");
                }

                response.EnsureSuccessStatusCode();

            }
        }
        public static async void Put(string url, object data)
        {
            using HttpClient httpClient = new HttpClient();
            {
               
                // Serialize the updated Pokemon object to JSON
                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Send the PUT request
                var response = await httpClient.PutAsync(url, content);
                
            }
        }
    }
}
