using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;
using ETL.Api.Application.Hashes.Contracts;
using ETL.Shared.Messages;
using MassTransit;

namespace ETL.Api.Application.Hashes;

public class HashService : IHashService
{
    private readonly IPublishEndpoint _publishEndpoint;

    public HashService(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task GenerateAsync()
    {
        var hashCount = Enumerable.Range(0, 40000);
        var chunks = hashCount.Chunk(1000);

        await Parallel.ForEachAsync(chunks, async (items, ct) =>
        {
            var shaList = new ConcurrentBag<string>();
            Parallel.ForEach(items, x =>
            {
                using var sha1 = SHA1.Create();
                var hash = Convert.ToHexString(sha1.ComputeHash(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString())));
                shaList.Add(hash);
            });

            var message = new HashesCreatedMessage { Hashes = shaList };
            await _publishEndpoint.Publish(message, ct);
        });
    }
    
    
}