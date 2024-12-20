using MassTransit;
using ProjetoRabbitMQ.Bus;
using ProjetoRabbitMQ.Extensions;
using ProjetoRabbitMQ.Relatorios;

namespace ProjetoRabbitMQ.Controllers;

internal static class ApiEndpoints
{
    public static void AddApiEndpoint(this WebApplication app)
    {
        //app.MapGet("hello world", () => new {saudacao = "hello world"});

        app.MapPost("solicitar-relatorio/{name}", async (string name, IPublishBus bus, CancellationToken ct=default) =>
        {
            var solicitacao = new SolicitacaoRelatorio()
            {
                Id = Guid.NewGuid(),
                Nome = name,
                Status = "Pendente",
                ProcessedTime = null
            };
            //Simulando Salvar no banco de dados
            Lista.Relatorios.Add(solicitacao);
            
            var eventRequest = new RelatorioSolicitadoEvent(solicitacao.Id, solicitacao.Nome);
            
            await bus.PublishAsync(eventRequest, ct);

            return Results.Ok(solicitacao);
        });

        app.MapGet("relatorios", () => Lista.Relatorios);

    }
}