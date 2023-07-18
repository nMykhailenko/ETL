using ETL.Consumer.Consumers;
using ETL.Consumer.Consumers.Definitions;
using ETL.Shared.Infrastructure.Persistence.Extensions;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<HashesCreatedConsumer, HashesCreatedConsumerDefinition>();
        
    x.SetKebabCaseEndpointNameFormatter();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://rabbitmq", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        
        cfg.ConfigureEndpoints(context);
    });
});
builder.Services.AddPersistence(builder.Configuration, false);


var app = builder.Build();


app.Run();