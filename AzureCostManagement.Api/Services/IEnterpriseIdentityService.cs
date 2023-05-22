using AzureCostManagement.Api.Models;
namespace AzureCostManagement.Api
{
    public interface IEnterpriseIdentityService
    {
        Task<AccessToken> GetAccessToken(string scope);
    }
}