using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using AzureCostManagement.Api.Models;

namespace AzureCostManagement.Api
{
    public class AzureCostManagementService : ICostManagementService
    {
        private readonly IEnterpriseIdentityService _enterpriseIdentityService;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public AzureCostManagementService(
            IEnterpriseIdentityService enterpriseIdentityService, 
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory)
        {
            _enterpriseIdentityService = enterpriseIdentityService;
            _configuration = configuration;
            _httpClient = httpClientFactory.CreateClient();
        }
        public async Task<CostManagementResult> GetUsageCostsAync(Guid subscriptionId, DateTime endDate, DateTime startDate)
        {
            var url = _configuration.GetSection("Application:AzureCostManagementUrlResource").Value!;
            var path = _configuration.GetSection("Application:AzureCostManagementUrlpath").Value!;

            var acessToken = await _enterpriseIdentityService.GetAccessToken(url);

            var request = new CostManagementRequestBuilder()
                .WithStartDate(startDate)
                .WithEndDate(endDate)
                .Build();

            var json = JsonSerializer.Serialize(request);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var fullUrl = string.Format(
                $"{url}{path}",
                subscriptionId);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", acessToken.Token);

            var response = await _httpClient.PostAsync(fullUrl, data);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<CostManagementResult>(result)!;
        }
    }
}