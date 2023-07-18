using ETL.Api.Application.Hashes.Contracts;
using ETL.Api.Application.Hashes.Extensions;
using ETL.Api.Application.Hashes.Models.Responses;
using ETL.Shared.Infrastructure.Persistence;
using ETL.Shared.Infrastructure.Persistence.Extensions;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((_, cfg) =>
    {
        cfg.Host("rabbitmq://rabbitmq", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });    
    });
});

builder.Services.AddHashes();
builder.Services.AddPersistence(builder.Configuration);

var app = builder.Build();

app.MapPost("/hashes", async (IHashService hashService) =>
{
    await hashService.GenerateAsync();
    return Results.Accepted();
});

app.MapGet("/hashes", async (ApplicationDbContext context) =>
{
    var hashesGrouped = await context.Hashes
        .GroupBy(x => x.Date.Date)
        .Select(x => new HashResponse
        {
            Date = x.Key.Date.ToString("yyyy-mm-dd"),
            Count = x.Count()
        })
        .ToListAsync();

    var response = new HashesResponse { Hashes = hashesGrouped };
    return Results.Ok(response);
});

app.Run();