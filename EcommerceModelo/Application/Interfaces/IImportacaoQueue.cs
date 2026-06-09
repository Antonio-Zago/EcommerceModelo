using Application.BackgroundJobs;

namespace Application.Interfaces;

public interface IImportacaoQueue
{
    void Enfileirar(ImportJob job);
    IAsyncEnumerable<ImportJob> LerAsync(CancellationToken ct);
}
