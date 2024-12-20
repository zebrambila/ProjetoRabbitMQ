﻿namespace ProjetoRabbitMQ.Relatorios;

internal static class Lista
{
    public static  List<SolicitacaoRelatorio> Relatorios = new();
}

public class SolicitacaoRelatorio
{
    public Guid Id { get; set; }
    public string Nome { get; set; } 
    public string Status { get; set; }
    public DateTime? ProcessedTime { get; set; }
}