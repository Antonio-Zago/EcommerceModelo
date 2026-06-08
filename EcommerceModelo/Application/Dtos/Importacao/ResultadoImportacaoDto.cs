namespace Application.Dtos.Importacao;

public class ResultadoImportacaoDto
{
    public int ProdutosCadastrados { get; set; }
    public List<string> ProdutosImportados { get; set; } = [];
    public List<string> Erros { get; set; } = [];
    public bool Sucesso => Erros.Count == 0;
}
