using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Press any key to start!");

            Console.ReadKey();

            Console.WriteLine("\nGetting Access Token...\n");

            var accessToken = await GetAccessTokenAsync();

            Console.WriteLine($"Access Token: {accessToken}");

            Console.WriteLine("\nGetting Values from the Rest API...\n");

            var response = await GetValues(accessToken);

            Console.WriteLine($"\n{response}");
        }

        private static async Task<string> GetAccessTokenAsync()
        {
            var client = new HttpClient();

            // discover endpoints from metadata
            var discover = await client.GetDiscoveryDocumentAsync("http://localhost:5000").ConfigureAwait(false);
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
            }).ConfigureAwait(false);

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

            var response = await client.GetAsync("http://localhost:52618/api/values").ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return response.StatusCode.ToString();
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                return JArray.Parse(content).ToString();
            }
        }
    }
}