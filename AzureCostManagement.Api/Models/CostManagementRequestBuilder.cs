namespace AzureCostManagement.Api.Models
{
    public class CostManagementRequestBuilder
    {
        private DateTime? _startDate;

        private DateTime? _endDate;

        public CostManagementRequestBuilder WithStartDate(DateTime startDate)
        {
            _startDate = startDate;
            return this;
        }

        public CostManagementRequestBuilder WithEndDate(DateTime endDate)
        {
            _endDate = endDate;
            return this;
        }

        public CostManagementRequest Build()
        {
            if (!_startDate.HasValue)
                throw new ArgumentException("Start date must be provided");

            if (!_endDate.HasValue)
                throw new ArgumentException("End date must be provided");

            return new CostManagementRequest
            {
                Type = "Usage",
                IncludeActualCost = true,
                IncludeFreshPartialCost = false,
                DataSet = new CostManagementDataSet
                {
                    Granularity = "Daily",
                    Aggregation = new CostManagementAggregation
                    {
                        TotalCostUSD = new CostManagementTotalCost
                        {
                            Name = "CostUSD",
                            Function = "Sum"
                        }
                    }
                },
                Timeframe = "Custom",
                TimePeriod = new CostManagementTimePeriod
                {
                    To = _endDate.Value,
                    From = _startDate.Value
                }
            };
        }
    }
}