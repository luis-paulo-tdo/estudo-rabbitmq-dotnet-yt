using ProjetoRabbitMq.Reports;

namespace ProjetoRabbitMq.Controllers;

public static class ApiEndpoints
{
    public static void AddApiEndpoints(this WebApplication app)
    {
        app.MapPost("solicitar-relatorio/{name}", (string name) =>
        {
            var solicitation = new ReportSolicitation()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Status = "Pendente",
                ProcessedTime = null
            };

            ReportList.Reports.Add(solicitation);
        });
    }
}
