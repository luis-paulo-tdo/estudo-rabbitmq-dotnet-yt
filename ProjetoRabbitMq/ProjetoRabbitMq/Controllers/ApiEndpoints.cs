using MassTransit;
using ProjetoRabbitMq.Reports;

namespace ProjetoRabbitMq.Controllers;

public static class ApiEndpoints
{
    public static void AddApiEndpoints(this WebApplication app)
    {
        app.MapGet("relatorios", () => ReportList.Reports);

        // TODO: Criar um Adapter/Wrapper para injetar o IBus
        app.MapPost("solicitar-relatorio/{name}", async (string name, IBus bus) =>
        {
            var solicitation = new ReportSolicitation()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Status = "Pendente",
                ProcessedTime = null
            };

            ReportList.Reports.Add(solicitation);

            var eventRequest = new RequestedReportEvent(solicitation.Id, solicitation.Name);

            await bus.Publish(eventRequest);

            return Results.Ok(solicitation);
        });
    }
}
