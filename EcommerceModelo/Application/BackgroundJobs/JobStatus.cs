namespace Application.BackgroundJobs;

public class JobStatus
{
    public Guid Id { get; init; }
    public string NomeArquivo { get; init; } = string.Empty;
    public string Status { get; set; } = StatusImportacao.Aguardando;
    public int Total { get; set; }
    public int Processados { get; set; }
    public List<string> ProdutosImportados { get; set; } = [];
    public List<string> Erros { get; set; } = [];
    public DateTime CriadoEm { get; init; } = DateTime.Now;
    public DateTime? ConcluidoEm { get; set; }
}

public static class StatusImportacao
{
    public const string Aguardando  = "aguardando";
    public const string Processando = "processando";
    public const string Concluido   = "concluido";
    public const string Falhou      = "falhou";
}
