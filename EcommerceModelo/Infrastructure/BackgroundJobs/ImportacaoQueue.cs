using Application.BackgroundJobs;
using Application.Interfaces;
using System.Runtime.CompilerServices;
using System.Threading.Channels;

namespace Infrastructure.BackgroundJobs;

public class ImportacaoQueue : IImportacaoQueue
{
    private readonly Channel<ImportJob> _channel =
        Channel.CreateUnbounded<ImportJob>(new UnboundedChannelOptions { SingleReader = true });

    public void Enfileirar(ImportJob job) =>
        _channel.Writer.TryWrite(job);

    public async IAsyncEnumerable<ImportJob> LerAsync(
        [EnumeratorCancellation] CancellationToken ct)
    {
        await foreach (var job in _channel.Reader.ReadAllAsync(ct))
            yield return job;
    }
}
