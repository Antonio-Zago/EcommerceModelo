using Application.BackgroundJobs;

namespace Application.Interfaces;

public interface IJobStore
{
    JobStatus Criar(Guid id, string nomeArquivo);
    JobStatus? Obter(Guid id);
    IReadOnlyList<JobStatus> ObterTodos();
    void Atualizar(Guid id, Action<JobStatus> update);
}
