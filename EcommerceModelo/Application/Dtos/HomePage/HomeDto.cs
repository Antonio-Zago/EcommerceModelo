namespace Application.Dtos.HomePage;

public class HomeDto
{
    public List<CategoriaDto> Categorias { get; set; } = new();
    public List<ProdutoDto> MaisVendidos { get; set; } = new();
    public List<ProdutoDto> Novidades { get; set; } = new();
    public List<ProdutoDto> Recomendados { get; set; } = new();
}
