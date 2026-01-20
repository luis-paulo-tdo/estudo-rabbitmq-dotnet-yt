namespace ProjetoRabbitMq.Controllers;

public static class ApiEndpoints
{
    public static void AddApiEndpoints(this WebApplication app)
    {
        app.MapGet("hello-world", () => new { saudacao = "Hello World!" });
    }
}
