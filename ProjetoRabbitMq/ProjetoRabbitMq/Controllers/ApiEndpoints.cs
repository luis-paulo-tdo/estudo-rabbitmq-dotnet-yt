using MassTransit;
using ProjetoRabbitMq.Bus;
using ProjetoRabbitMq.Reports;

namespace ProjetoRabbitMq.Controllers;

public static class ApiEndpoints
{
    public static void AddApiEndpoints(this WebApplication app)
    {
        app.MapGet("relatorios", () => ReportList.Reports);

        app.MapPost("solicitar-relatorio/{name}", async (string name, IPublishBus bus, CancellationToken ct = default) =>
        {
            var solicitation = new ReportSolicitation()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Status = "Pendente",
                ProcessedTime = null
            };

            // Persistência fictícia
            ReportList.Reports.Add(solicitation);

            var eventRequest = new RequestedReportEvent(solicitation.Id, solicitation.Name);

            await bus.PublishAsync(eventRequest, ct);

            return Results.Ok(solicitation);
        });
    }
}
