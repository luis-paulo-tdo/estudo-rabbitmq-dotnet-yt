namespace ProjetoRabbitMq.Reports;

internal static class ReportList
{
    public static List<ReportSolicitation> Reports = new();
}

public class ReportSolicitation
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public DateTime? ProcessedTime { get; set; }
}
