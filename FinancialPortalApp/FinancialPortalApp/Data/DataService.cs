using Newtonsoft.Json;
using Org.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FinancialPortalApp.Data
{
    class DataService
    {
        public static async Task<string> GetDataFromServiceAsync(string queryString)
        {
            var data = string.Empty;
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(queryString).ConfigureAwait(false);
                    if (response != null)
                    {
                        data = response.Content.ReadAsStringAsync().Result;
                    }
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                    Console.WriteLine(error);
                }
            }
            return data;
        }

        public static async void PostDataServiceAsync(string queryString)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(queryString);

            try
            {
                HttpResponseMessage response = await client.PostAsync(queryString, null);
                var result = await response.Content.ReadAsStringAsync();
            }
            catch (Exception er)
            {
                Console.WriteLine(er);
            }
        }

        public static async void PutDataServiceAsync(string queryString)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(queryString);

            try
            {
                HttpResponseMessage response = await client.PutAsync(queryString, null);
                var result = await response.Content.ReadAsStringAsync();
            }
            catch (Exception er)
            {
                Console.WriteLine(er);
            }
        }
        public static async void DeleteDataServiceAsync(string queryString)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(queryString);

            try
            {
                HttpResponseMessage response = await client.DeleteAsync(queryString);
                var result = await response.Content.ReadAsStringAsync();
            }
            catch (Exception er)
            {
                Console.WriteLine(er);
            }
        }
    }
}
