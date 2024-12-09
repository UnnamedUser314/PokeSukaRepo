﻿using PokemonBackRules.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PokemonBackRules.Services
{
    public class HttpJsonClientService<T> : IHttpJsonClientProvider<T> where T : class
    {
        public async Task<T?> Get(string url)
        {
            try
            {
                using HttpClient httpClient = new HttpClient();
                {
                    HttpResponseMessage datos = await httpClient.GetAsync(url);
                    string dataget = await datos.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<T>(dataget);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default(T?);
            }
        }
        public async Task<List<T?>> GetAll(string url)
        {
            using HttpClient httpClient = new HttpClient();
            {
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<T>>(responseBody);
            }
        }
        public async Task Post(string url, object data)
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
        public async Task Put(string url, object data)
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
        public async Task DeleteAll(string url)
        {
            using HttpClient httpClient = new HttpClient();
            {
                var response = await httpClient.DeleteAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    string errorDetails = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {response.StatusCode}");
                    Console.WriteLine($"Details: {errorDetails}");
                }

                response.EnsureSuccessStatusCode();
            }
        }
    }
}