using AzureCostManagement.Api.Models;

namespace AzureCostManagement.Api
{
    public interface ICostManagementService
    {
        Task<CostManagementResult> GetUsageCostsAync(Guid subscriptionId, DateTime endDate, DateTime startDate);
    }
}