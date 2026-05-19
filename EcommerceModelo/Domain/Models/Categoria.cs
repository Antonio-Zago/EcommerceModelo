public class Categoria
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;

    public string ImagemBase64 { get; set; }

    public ICollection<CategoriaProduto> CategoriasProdutos { get; set; } = new List<CategoriaProduto>();
}