namespace ETL.Shared.Entities;

public class HashEntity
{
    public long Id { get; init; }
    public string Hash { get; init; } = null!;
    public DateTime Date { get; init; }
}