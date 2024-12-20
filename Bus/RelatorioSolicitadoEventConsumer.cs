using MassTransit;
using ProjetoRabbitMQ.Extensions;
using ProjetoRabbitMQ.Relatorios;

namespace ProjetoRabbitMQ.Bus;

internal sealed class RelatorioSolicitadoEventConsumer : IConsumer<RelatorioSolicitadoEvent>
{
    private readonly ILogger<RelatorioSolicitadoEventConsumer> _logger;

    public RelatorioSolicitadoEventConsumer(ILogger<RelatorioSolicitadoEventConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<RelatorioSolicitadoEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Processando Relatório Id:{Id}, Nome: {Nome}", message.Id, message.Name);
        
        //Delay
        await Task.Delay(10000);
        
        //Atualizando o Status
        var relatorio = Lista.Relatorios.FirstOrDefault(x => x.Id == message.Id);

        if (relatorio != null)
        {
            relatorio.Status = "Completado!";
            relatorio.ProcessedTime = DateTime.Now;
        }
        _logger.LogInformation("Relatório Processado Id:{Id}, Nome: {Nome}", message.Id, message.Name);

    }
}