namespace ETL.Api.Application.Hashes.Contracts;

public interface IHashService
{
    Task GenerateAsync();
}