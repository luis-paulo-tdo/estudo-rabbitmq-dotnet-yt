using MassTransit;

namespace ProjetoRabbitMq.Bus
{
    internal interface IPublishBus
    {
        Task PublishAsync<T>(T message, CancellationToken ct = default) where T : class;
    }

    internal class PublishBus : IPublishBus
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public PublishBus(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public Task PublishAsync<T>(T message, CancellationToken ct = default) where T : class
        {
            return _publishEndpoint.Publish(message, ct);
        }
    }
}
