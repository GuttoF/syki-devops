using System.ComponentModel;

namespace Syki.Shared;

/// <summary>
/// Status de um Lote de comandos
/// </summary>
public enum CommandBatchStatus
{
    [Description("Pendente")]
    Pending,

    [Description("Processando")]
    Processing,

    [Description("Sucesso")]
    Success,

    [Description("Erro")]
    Error,
}
