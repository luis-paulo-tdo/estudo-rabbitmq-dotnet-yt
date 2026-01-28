using MassTransit;
using ProjetoRabbitMq.Reports;

namespace ProjetoRabbitMq.Bus
{
    internal sealed class RequestedReportEventConsumer : IConsumer<RequestedReportEvent>
    {
        private readonly ILogger<RequestedReportEventConsumer> _logger;

        public RequestedReportEventConsumer(ILogger<RequestedReportEventConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<RequestedReportEvent> context)
        {
            var message = context.Message;

            if (message.Name.Length <= 3)
            {
                throw new Exception("Nome inválido");
            }

            _logger.LogInformation("Processando relatório ID: {Id}, Nome: {Nome}", message.Id, message.Name);

            // Delay
            await Task.Delay(10000);

            // Atualizando status
            var relatorio = ReportList.Reports.FirstOrDefault(r => r.Id == message.Id);

            if (relatorio != null)
            {
                relatorio.Status = "Completo";
                relatorio.ProcessedTime = DateTime.UtcNow;
            }

            _logger.LogInformation("Relatório processado ID: {Id}, Nome: {Nome}", message.Id, message.Name);
        }
    }
}
