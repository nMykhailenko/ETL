using System.Text.Json.Serialization;

namespace ETL.Api.Application.Hashes.Models.Responses;

public record HashesResponse
{
    [JsonPropertyName("hashes")]
    public IReadOnlyCollection<HashResponse> Hashes { get; init; }
}