using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start!");

            Console.ReadKey();

            Console.WriteLine("\nProcessing...\n");

            var accessToken = Task.Run( () => GetAccessTokenAsync()).Result;

            Console.WriteLine($"Access Token: {accessToken}");

            var response = Task.Run(() => GetValues(accessToken)).Result;

            Console.WriteLine($"\n{response}");
        }

        private static async Task<string> GetAccessTokenAsync()
        {
            var client = new HttpClient();

            // discover endpoints from metadata
            var discover = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
            if (discover.IsError)
            {
                return discover.Error;
            }

            // request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discover.TokenEndpoint,
                ClientId = "consoleClient",
                ClientSecret = "secret",
                Scope = "api1"
            });

            if (tokenResponse.IsError)
            {
                return tokenResponse.Error;
            }

            return tokenResponse.AccessToken;
        }

        private static async Task<string> GetValues(string accessToken)
        {
            // call api. TODO change the HttpClient by the HttpClienFactory
            var client = new HttpClient();

            client.SetBearerToken(accessToken);

            var response = await client.GetAsync("http://localhost:52618/api/values");

            if (!response.IsSuccessStatusCode)
            {
                return response.StatusCode.ToString();
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();

                return JArray.Parse(content).ToString();
            }
        }
    }
}