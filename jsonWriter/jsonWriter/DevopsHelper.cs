using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace jsonWriter
{
    public class DevopsHelper
    {
        public string clientId = "1537b283-b86b-468e-8d2e-1408910252d3";
        public string clientSecret = "QpNG0jfSIw5Q1OAltxQyog8hi_U.Sm35s4";
        public string tenant = "b4741a82-6b6e-43a5-ad6e-1044511aaed6";
        public string acessToken = "dcoq2dtdzppyq763x4pxsudambeivz3dfk23nyioqwvtu4n2cmjq";

        public void PrintAuthCode()
        {
            Console.WriteLine(GetAuthorizationCode());
        }

        public static async Task<string> MakeRequestAsync(HttpRequestMessage getRequest, HttpClient client)
        {
            var response = await client.SendAsync(getRequest).ConfigureAwait(false);
            var responseString = string.Empty;
            try
            {
                response.EnsureSuccessStatusCode();
                Console.WriteLine("RESSSSSSSPONNNSeee: "+response.StatusCode.ToString());
                responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
            catch (HttpRequestException)
            {

            }

            return responseString;
        }

        public async Task<string> GetVariables()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://dev.azure.com/Gatofeles/Start/_apis/distributedtask/variablegroups?api-version=6.0-preview.2");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + acessToken);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, client.BaseAddress);
            var response = await MakeRequestAsync(request, client);
            
            return response;
        }

        public string GetAuthorizationCode()
        {
            ClientCredential cc = new ClientCredential(clientId, clientSecret);
            var context = new AuthenticationContext("https://login.microsoftonline.com/" + tenant);
            var result = context.AcquireTokenAsync("https://management.azure.com/", cc);
           
            if(result == null)
            {
                throw new Exception("Invalido");
            }

            return result.Result.AccessToken;
        }
    }
}
