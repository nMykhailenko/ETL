using System.Text.Json.Serialization;

namespace ETL.Api.Application.Hashes.Models.Responses;

public record HashResponse
{
    [JsonPropertyName("date")]
    public string Date { get; init; }
    
    [JsonPropertyName("count")]
    public long Count { get; init; }
}