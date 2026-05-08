public class ProdutoImagem
{
    public int Id { get; set; }
    public string ImagemBase64 { get; set; } = string.Empty;

    public int ProdutoId { get; set; }
    public Produto Produto { get; set; } = null!;
}