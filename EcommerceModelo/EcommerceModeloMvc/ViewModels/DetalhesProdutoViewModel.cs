namespace EcommerceModeloMvc.ViewModels;

public class DetalhesProdutoViewModel
{
    public int Id { get; init; }
    public string Nome { get; init; } = string.Empty;
    public decimal Preco { get; init; }
    public string Descricao { get; init; } = string.Empty;
    public string Categoria { get; init; } = string.Empty;
    public string Genero { get; init; } = string.Empty;
    public bool EhInfantil { get; init; }
    public List<ImagemViewModel> Imagens { get; init; } = [];
    public List<EstoqueViewModel> Estoques { get; init; } = [];
}

public class ImagemViewModel
{
    public string Url { get; init; } = string.Empty;
    public bool Principal { get; init; }
}

public class EstoqueViewModel
{
    public int TamanhoId { get; init; }
    public string Tamanho { get; init; } = string.Empty;
    public int Quantidade { get; init; }
}
