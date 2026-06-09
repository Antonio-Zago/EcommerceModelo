using Application.BackgroundJobs;
using Application.Interfaces;

namespace EcommerceModeloMvc.BackgroundJobs;

/// <summary>
/// Hosted service que processa jobs de importação em background.
/// Usa IServiceScopeFactory porque IImportacaoProdutosService é Scoped,
/// mas o worker é Singleton.
/// </summary>
public class ImportacaoWorker : BackgroundService
{
    private readonly IImportacaoQueue _queue;
    private readonly IJobStore _jobStore;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<ImportacaoWorker> _logger;

    public ImportacaoWorker(
        IImportacaoQueue queue,
        IJobStore jobStore,
        IServiceScopeFactory scopeFactory,
        ILogger<ImportacaoWorker> logger)
    {
        _queue = queue;
        _jobStore = jobStore;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        await foreach (var job in _queue.LerAsync(ct))
        {
            _logger.LogInformation("Iniciando importação do job {JobId} ({Arquivo})", job.Id, job.NomeArquivo);
            _jobStore.Atualizar(job.Id, s => s.Status = StatusImportacao.Processando);

            try
            {
                using var scope = _scopeFactory.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<IImportacaoProdutosService>();

                var progresso = new Progress<(int processados, int total)>(p =>
                    _jobStore.Atualizar(job.Id, s =>
                    {
                        s.Processados = p.processados;
                        s.Total = p.total;
                    }));

                var resultado = await service.ImportarAsync(job.Linhas, job.WebRootPath, progresso);

                _jobStore.Atualizar(job.Id, s =>
                {
                    s.Status = StatusImportacao.Concluido;
                    s.ProdutosImportados = resultado.ProdutosImportados;
                    s.Erros = resultado.Erros;
                    s.Processados = resultado.ProdutosCadastrados + resultado.Erros.Count;
                    s.ConcluidoEm = DateTime.Now;
                });

                _logger.LogInformation(
                    "Job {JobId} concluído: {Importados} importados, {Erros} erros.",
                    job.Id, resultado.ProdutosCadastrados, resultado.Erros.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha no job {JobId}", job.Id);
                _jobStore.Atualizar(job.Id, s =>
                {
                    s.Status = StatusImportacao.Falhou;
                    s.Erros = [$"Erro interno: {ex.Message}"];
                    s.ConcluidoEm = DateTime.Now;
                });
            }
        }
    }
}
