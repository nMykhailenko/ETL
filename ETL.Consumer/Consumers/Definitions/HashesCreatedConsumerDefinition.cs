using MassTransit;

namespace ETL.Consumer.Consumers.Definitions;

public class HashesCreatedConsumerDefinition : ConsumerDefinition<HashesCreatedConsumer>
{
    public HashesCreatedConsumerDefinition()
    {
        EndpointName = "create-hashes";
        ConcurrentMessageLimit = 4;
    }
    
    protected void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<HashesCreatedConsumerDefinition> consumerConfigurator)
    {
        endpointConfigurator.PrefetchCount = 4;
        
        endpointConfigurator.UseInMemoryOutbox();
    }
}