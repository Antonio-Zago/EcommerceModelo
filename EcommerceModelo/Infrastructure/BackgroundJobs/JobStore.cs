using Application.BackgroundJobs;
using Application.Interfaces;
using System.Collections.Concurrent;

namespace Infrastructure.BackgroundJobs;

public class JobStore : IJobStore
{
    private readonly ConcurrentDictionary<Guid, JobStatus> _jobs = new();

    public JobStatus Criar(Guid id, string nomeArquivo)
    {
        var status = new JobStatus { Id = id, NomeArquivo = nomeArquivo };
        _jobs[id] = status;
        return status;
    }

    public JobStatus? Obter(Guid id) =>
        _jobs.TryGetValue(id, out var status) ? status : null;

    public IReadOnlyList<JobStatus> ObterTodos() =>
        _jobs.Values.OrderByDescending(j => j.CriadoEm).ToList();

    public void Atualizar(Guid id, Action<JobStatus> update)
    {
        if (_jobs.TryGetValue(id, out var status))
            lock (status) { update(status); }
    }
}
