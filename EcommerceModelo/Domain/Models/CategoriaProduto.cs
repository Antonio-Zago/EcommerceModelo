public class CategoriaProduto
{
    public int ProdutoId { get; set; }
    public Produto Produto { get; set; } = null!;

    public int CategoriaId { get; set; }
    public Categoria Categoria { get; set; } = null!;
}