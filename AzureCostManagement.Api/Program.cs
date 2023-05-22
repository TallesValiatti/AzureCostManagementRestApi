using AzureCostManagement.Api;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICostManagementService, AzureCostManagementService>();
builder.Services.AddScoped<IEnterpriseIdentityService, AzureAdService>();
builder.Services.AddHttpClient();

var app = builder.Build();

app.MapGet("/cost-management", async (
    [FromQuery] Guid subscritionId,
    ICostManagementService costManagementService) => 
{
    var today = DateTime.Today;

    var firstDayOfCurrentMonth = new DateTime(today.Year, today.Month, 1);
    var lastDayOfCurrentMonth = firstDayOfCurrentMonth.AddMonths(1).AddDays(-1);

    var data = await costManagementService.GetUsageCostsAync(
        subscritionId, 
        lastDayOfCurrentMonth, 
        firstDayOfCurrentMonth);

    return Results.Ok(new 
    {
        Values = data.Properties.RowsFormated
    });
 });

app.Run();
