using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using APIOnlineShop.BO;
using Newtonsoft.Json;
using shoppingPortal;

namespace APIOnlineShop.BO
{
    public class ConsumeEventSync
    {
        //public async Task<List<Product>> GetAllEventData(string endpoint) 
        //{
        //    using (var client = new HttpClient()) //WebClient  
        //    {
        //        List<Product> listProducts = new List<Product>();
        //        client.BaseAddress = new Uri("https://localhost:44381/api/");
        //        var responseTask = client.GetAsync(endpoint);
        //        responseTask.Wait();

        //        var result = responseTask.Result;
        //        if (result.IsSuccessStatusCode)
        //        {
        //            listProducts = JsonConvert.DeserializeObject<List<Product>>(
        //            await result.Content.ReadAsStringAsync());
        //        }
        //        return listProducts;
        //    }

        //}

       //public  async Task<List<T>> GetAllEventData<T>(string endpoint)
       // {
       //     using (var client = new HttpClient()) //WebClient  
       //     {
              
       //         List<T> listT = new List<T>();
       //         client.BaseAddress = new Uri("https://localhost:44381/api/");
       //         var responseTask = client.GetAsync(endpoint);
       //         responseTask.Wait();

       //         var result = responseTask.Result;
       //         if (result.IsSuccessStatusCode)
       //         {
       //             listT = JsonConvert.DeserializeObject<List<T>>(
       //             await result.Content.ReadAsStringAsync());
       //         }
       //         return listT;
       //     }
       // }


       // public async Task<T> GetOne<T>(string endpoint)
       // {
       //     using (var client = new HttpClient()) //WebClient  
       //     {
       //         T t;
       //         client.BaseAddress = new Uri("https://localhost:44381/api/");
       //         var responseTask = client.GetAsync(endpoint);
       //         responseTask.Wait();

       //         var result = responseTask.Result;
       //         if (result.IsSuccessStatusCode)
       //         {
       //             t = JsonConvert.DeserializeObject<T>(
       //             await result.Content.ReadAsStringAsync());
       //             return t;
       //         }
       //         else return default(T);

       //     }
       // }


       // public async Task<AuthenticationResponse> GetSession<AuthenticationResponse>(LoginCredentials loginCredentials)
       // {
       //     AuthenticationResponse response;
       //     using (var client = new HttpClient()) //WebClient  
       //     {
       //         var responseTask = client.PostAsJsonAsync("https://localhost:44381/api/person/authenticate", loginCredentials);
       //         responseTask.Wait();

       //         var result = responseTask.Result;
       //         if (result.IsSuccessStatusCode)
       //         {
       //             response = JsonConvert.DeserializeObject<AuthenticationResponse>(
       //             await result.Content.ReadAsStringAsync());
       //             return response;
       //         }
       //         else
       //         {
       //             return default(AuthenticationResponse);
       //         }
                
       //     }
       // }

    }
}
