using LE.Cafe.HelperClass;
using LE.Cafe.Models;
using LE.Cafe.Services.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LE.Cafe.Services.Implementation
{
    public class ApiServiceImpl : ApiServices
    {
        private JsonSerializer _serializer= new JsonSerializer();
        private HttpClient client = new HttpClient();
        public async Task<ObservableCollection<T>> GetResultDataAsync<T>(string url, string token, bool forceRefresh = false) =>
           await GetAsync<ObservableCollection<T>>(url, token);


        //public async Task<ObservableCollection<T>> GetResultDataAsync<T>(string url)
        //{
        //    try
        //    {
        //        if (ConnectionHelper.IsInternetConnectionAvailable() == false)
        //        {
        //            throw new Exception("No internet connection available");
        //        }
        //        var Base = new Uri(HttpHelper.BaseUri);
        //        var RelativeUri = new Uri(url);
        //        var FullUrl = new Uri(Base, RelativeUri);
        //        var request = new RestRequest(FullUrl, method: Method.GET);
        //        var response = await HttpHelper.GetClient().ExecuteAsync(request);
        //        var result = JsonConvert.DeserializeObject<ResponseModel<T>>(response.Content);
        //        return result.Data;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}


        public async Task<T> GetSingleResultDataAsync<T>(string uri, bool forceRefresh = false) =>
            await GetSingleAsync<T>(uri);


        //public async Task<T> GetSingleResultDataAsync<T>(string uri)
        //{
        //    try
        //    {
        //        if (ConnectionHelper.IsInternetConnectionAvailable() == false)
        //        {
        //            throw new Exception("No internet connection available");
        //        }
        //        //var request = new RestRequest(uri, method: Method.GET);
        //        //var response  = await HttpHelper.GetClient().ExecuteAsync(request);
        //        var Base = new Uri(HttpHelper.BaseUri);
        //        var RelativeUri = new Uri(uri);
        //        var FullUrl = new Uri(Base, RelativeUri);
        //        var request = new RestRequest(FullUrl, method: Method.GET);
        //        var response = await HttpHelper.GetClient().ExecuteAsync(request);
        //        var result = JsonConvert.DeserializeObject<T>(response.Content);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        public async Task<T> PostAsync<T>(string baseUrl, string uri, string username, string password)
        {
            var Base = new Uri(baseUrl);
            var FullUrl = new Uri(Base, uri);
            var request = new RestRequest(FullUrl, Method.POST);
            var loginDetails = new Login()
            {
                Username = username,
                Password = password
            };
            request.AddJsonBody(loginDetails);
            //  request.AddHeader("Authentication", "token");//Example
            var response = await HttpHelper.GetClient().ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<T>(response.Content);
            }
           return default(T);
        }

        public async Task<bool> PostDataAsync<T>(string baseUrl,TableOrder tableOrder)
        {
            
            var request = new RestRequest(baseUrl, Method.POST);
           
            request.AddJsonBody(tableOrder);
            //  request.AddHeader("Authentication", "token");//Example
            var response = await HttpHelper.GetClient().ExecuteAsync(request);
            return response.IsSuccessful;
        }

        public async Task<ObservableCollection<T>> GetDataAsync<T>(string uri, string token = "")
        {
            client = CreateHttpClient(token);
            HttpResponseMessage response = await client.GetAsync(uri);
            await HandleResponse(response);
            string serialized = await response.Content.ReadAsStringAsync();
            ObservableCollection<T> result = await Task.Run(() =>
            JsonConvert.DeserializeObject<ObservableCollection<T>>(serialized));
            return result;
        }
        async Task HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Response Failed!!");
            }
        }
        private HttpClient CreateHttpClient(string token = "")
        {
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
            }
            return client;
        }

        public async Task<T> GetAsync<T>(string url, string token, int minute = 60, bool forceRefresh = false)
        {


            var baseurl = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{token}");
            HttpResponseMessage response = await client.GetAsync($"{baseurl}");
            response.EnsureSuccessStatusCode();
            using (var stream = await response.Content.ReadAsStreamAsync())
            using (var reader = new StreamReader(stream))
            using (var json = new JsonTextReader(reader))
            {
                return _serializer.Deserialize<T>(json);
            }

            //var json = string.Empty;
            //  if (!ConnectionHelper.IsInternetConnectionAvailable())
            //    json = Barrel.Current.Get<string>(url);

            //if (!forceRefresh && !Barrel.Current.IsExpired(url))
            //    json = Barrel.Current.Get<string>(url);

            //try
            //{
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            //    HttpResponseMessage response = await client.GetAsync(url);
            //    await HandleResponse(response);
            //    string serialized = await response.Content.ReadAsStringAsync();
            //    // Barrel.Current.Add(url, json, TimeSpan.FromMinutes(minute));
            //    //}
            //    var res = JsonConvert.DeserializeObject<T>(serialized);
            //    return res;

            //    //using (HttpClient client = new HttpClient())
            //    //{
            //    //    //if (string.IsNullOrWhiteSpace(json))
            //    //   // {
            //    //       // Barrel.Current.Empty(url);
            //    //    //var request = new RestRequest(FullUrl, method: Method.GET);
            //    //    //json = await HttpHelper.GetClient().ExecuteAsync(request);

            //    //    // var storedData = Barrel.Current.Get<string>(url);
            //    //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    //    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            //    //    json = await client.GetStringAsync(url);

            //    //    // Barrel.Current.Add(url, json, TimeSpan.FromMinutes(minute));
            //    //    //}
            //    //    var res = JsonConvert.DeserializeObject<ResponseModel<T>>(json);
            //    //    return res.Data;
            //    //}
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Unable to get information from server {ex}");
            //    //probably re-throw here :)
            //}

            //return default(T);
        }
        public async Task<T> GetSingleAsync<T>(string url, int minute = 60, bool forceRefresh = false)
        {
            var json = string.Empty;
            //Barrel.Current.EmptyAll();
            //if (!ConnectionHelper.IsInternetConnectionAvailable())
            //    json = Barrel.Current.Get<string>(url);

            // if (!forceRefresh && !Barrel.Current.IsExpired(url))
            //     json = Barrel.Current.Get<string>(url);

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    if (string.IsNullOrWhiteSpace(json))
                    {
                        var Base = new Uri(HttpHelper.BaseUri);
                        var RelativeUri = new Uri(url);
                        var FullUrl = new Uri(Base, RelativeUri);
                        //var request = new RestRequest(FullUrl, method: Method.GET);
                        //json = await HttpHelper.GetClient().ExecuteAsync(request);

                        json = await client.GetStringAsync(FullUrl);
                        //Barrel.Current.Add(url, json, TimeSpan.FromMinutes(minute));
                    }
                    return JsonConvert.DeserializeObject<T>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to get information from server {ex}");
                //probably re-throw here :)
            }

            return default(T);
        }
    }
}
