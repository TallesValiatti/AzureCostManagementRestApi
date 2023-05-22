namespace AzureCostManagement.Api.Models
{
    public record CostManagementRequest
    {
        public string Type { get; init; } = default!;

        public CostManagementDataSet DataSet { get; init; } = default!;

        public string Timeframe { get; init; } = default!;

        public bool IncludeActualCost { get; init; }

        public bool IncludeFreshPartialCost { get; init; }

        public CostManagementTimePeriod TimePeriod { get; init; } = default!;        
    }

    public record CostManagementTimePeriod
    {
        public DateTime From { get; init; }
        public DateTime To { get; init; }
    }

    public record CostManagementDataSet
    {
        public string Granularity { get; init; } = default!;

        public CostManagementAggregation Aggregation { get; init; } = default!;
    }

     public record CostManagementAggregation
    {
        public CostManagementTotalCost TotalCostUSD { get; init; } = default!;
    }

    public record CostManagementTotalCost
    {
        public string Name { get; init; } = default!;

        public string Function { get; init; } = default!;
    }
}