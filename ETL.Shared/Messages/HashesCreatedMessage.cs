namespace ETL.Shared.Messages;

public record HashesCreatedMessage
{
    public HashesCreatedMessage()
    {
        Hashes = Array.Empty<string>();
    }
    
    public IReadOnlyCollection<string> Hashes { get; init; }
}