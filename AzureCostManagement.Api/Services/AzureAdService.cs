using System.Text.Json;
using AzureCostManagement.Api.Models;

namespace AzureCostManagement.Api
{
    public class AzureAdService : IEnterpriseIdentityService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public AzureAdService(IConfiguration configuration, IHttpClientFactory httpclientFactory)
        {
            _configuration = configuration;
            _httpClient = httpclientFactory.CreateClient();
        }
        public async Task<AccessToken> GetAccessToken(string scope)
        {
            var url = _configuration.GetSection("Application:AzureAdLoginUrlResource").Value!;
            var path = _configuration.GetSection("Application:AzureAdLoginUrlPath").Value!;

            var dict = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", _configuration.GetSection("Application:ClientId").Value! },
                { "client_secret", _configuration.GetSection("Application:ClientSecret").Value! },
                { "scope", $"{scope}/.default" }
            };
   
            var fullUrl = string.Format(
                $"{url}{path}",
                _configuration.GetSection("Application:TenantId").Value!);

            var requestBody = new FormUrlEncodedContent(dict);
            var response = await _httpClient.PostAsync(fullUrl, requestBody);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<AccessToken>(result)!;
        }
    }
}