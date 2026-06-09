using Application.Dtos.Importacao;

namespace Application.BackgroundJobs;

public class ImportJob
{
    public Guid Id { get; init; }
    public string NomeArquivo { get; init; } = string.Empty;
    public List<LinhaImportacaoDto> Linhas { get; init; } = [];
    public string WebRootPath { get; init; } = string.Empty;
}
