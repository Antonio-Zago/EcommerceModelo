namespace Application.Dtos.Importacao;

public class LinhaImportacaoDto
{
    public int NumeroLinha { get; init; }
    public string Nome { get; init; } = string.Empty;
    public string Preco { get; init; } = string.Empty;
    public string Descricao { get; init; } = string.Empty;
    public string Categoria { get; init; } = string.Empty;
    public string Genero { get; init; } = string.Empty;
    public string EhInfantil { get; init; } = string.Empty;
    public string TipoTamanho { get; init; } = string.Empty;
    public string OpcaoTamanho { get; init; } = string.Empty;
    public string QtdEstoque { get; init; } = string.Empty;
    public IReadOnlyList<string> UrlsImagens { get; init; } = [];
}
