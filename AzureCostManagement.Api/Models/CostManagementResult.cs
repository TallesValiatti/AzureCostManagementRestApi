using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AzureCostManagement.Api.Models
{
    public record CostManagementResult
    {
        [JsonPropertyName("id")]
        public string Id { get; init; } = default!;

        [JsonPropertyName("name")]
        public string Name { get; init; } = default!;

        [JsonPropertyName("type")]
        public string Type { get; init; } = default!;

        [JsonPropertyName("properties")]
        public CostManagementProperties Properties { get; init; } = default!;
    }

    public record CostManagementProperties
    {
        [JsonPropertyName("nextLink")]
        public string? NextLink { get; init; }

        [JsonPropertyName("columns")]
        public IEnumerable<CostManagementColumn> Columns { get; init; } = default!;

        [JsonPropertyName("rows")]
        public IEnumerable<IEnumerable<dynamic>> Rows { get; init; } = default!;

        public IEnumerable<CostManagementRow> RowsFormated
        {
            get
            {
                var dateFormat = "yyyyMMdd";

                return Rows.Select(x => new CostManagementRow
                {
                    // Amount
                    Amount = ((JsonElement)x.ToArray<object>()[0]).GetDouble(),

                    // Date
                    Date = DateTime.ParseExact(
                        ((JsonElement)x.ToArray<object>()[1]).GetInt64().ToString(),
                        dateFormat,
                        CultureInfo.InvariantCulture),

                    // CostStatus
                    CostStatus = ((JsonElement)x.ToArray<object>()[2]).GetString()!,

                    // Currency
                    Currency = ((JsonElement)x.ToArray<object>()[3]).GetString()!,
                });
            }
        }        
    }

    public record CostManagementRow
    {
        public double Amount { get; init; }

        public DateTime Date { get; init; }

        public string CostStatus { get; init; } = default!;

        public string Currency { get; init; } = default!;
    }

    public record CostManagementColumn
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("type")]
        public string Type { get; set; } = null!;
    }
}