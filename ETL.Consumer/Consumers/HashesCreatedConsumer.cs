using ETL.Shared.Entities;
using ETL.Shared.Infrastructure.Persistence;
using ETL.Shared.Messages;
using MassTransit;

namespace ETL.Consumer.Consumers;

public class HashesCreatedConsumer : IConsumer<HashesCreatedMessage>
{
    private readonly ApplicationDbContext _context;

    public HashesCreatedConsumer(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Consume(ConsumeContext<HashesCreatedMessage> context)
    {
        var entities = context.Message.Hashes.Select(x => new HashEntity
        {
            Date = DateTime.UtcNow,
            Hash = x
        });
        await _context.Hashes.AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }
}