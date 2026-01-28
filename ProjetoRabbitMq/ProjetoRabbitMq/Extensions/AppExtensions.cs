using MassTransit;
using ProjetoRabbitMq.Bus;

namespace ProjetoRabbitMq.Extensions
{
    public static class AppExtensions
    {
        public static void AddRabbitMqService(this IServiceCollection services)
        {
            services.AddTransient<IPublishBus, PublishBus>();

            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.AddConsumer<RequestedReportEventConsumer>();

                busConfigurator.UsingRabbitMq((context, configurator) =>
                {
                    // TODO: Obter endereço via appsettings
                    configurator.Host(new Uri("amqp://localhost:5672"), host =>
                    {
                        host.Username("guest");
                        host.Password("guest");

                        configurator.ConfigureEndpoints(context);
                    });
                });
            });
        }
    }
}
