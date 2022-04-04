using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.TeamFoundation.DistributedTask.WebApi;
using System.Collections.Generic;

namespace jsonWriter
{ 
    class Program
    {
        static void Main(string[] args)
        {

            string personalAccessToken = "dcoq2dtdzppyq763x4pxsudambeivz3dfk23nyioqwvtu4n2cmjq";
            VariableGroup convert = null;
            //encode your personal access token                   
            string credentials = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", "", personalAccessToken)));

            //use the httpclient
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"https://dev.azure.com/Gatofeles/Start/");  //url of your organization
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                //connect to the REST endpoint            
                HttpResponseMessage response = client.GetAsync("_apis/distributedtask/variablegroups/01?api-version=5.0-preview.1").Result;

                //check to see if we have a successful response
                if (response.IsSuccessStatusCode)
                {
                    //set the viewmodel from the content in the response
                    var viewModel = response.Content.ReadAsStringAsync();
                    var result = viewModel.GetAwaiter().GetResult();
                    convert = JsonConvert.DeserializeObject<VariableGroup>(result);
                    foreach (var item in convert.Variables)
                    {
                       if( item.Key.ToString() == "saviorName")
                        {
                            item.Value.Value = "Vegeta"+DateTime.Now.ToString();
                        }
                    }
                   

                    //var value = response.Content.ReadAsStringAsync().Result;
                }
            }



            //use the httpclient
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"https://dev.azure.com/Gatofeles/Start/");  //url of your organization
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                //connect to the REST endpoint
                var json = JsonConvert.SerializeObject(convert);

                JsonHelper jh = new JsonHelper();

                HttpResponseMessage response = client.PutAsJsonAsync("_apis/distributedtask/variablegroups/01?api-version=5.0-preview.1", jh.JsonReader()).Result;
                Console.WriteLine("Variable: "+response.StatusCode.ToString());
               
                if (response.IsSuccessStatusCode)
                {
                    //set the viewmodel from the content in the response
                    var viewModel = response.Content.ReadAsStringAsync();
                    var result = viewModel.GetAwaiter().GetResult();
                    
                    
                    Console.WriteLine(result);

                    //var value = response.Content.ReadAsStringAsync().Result;
                }
            }
        }
    }
}
