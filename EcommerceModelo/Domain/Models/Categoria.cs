using System.ComponentModel.DataAnnotations.Schema;

public class Categoria
{
    [Column("id")]
    public int Id { get; set; }

    [Column("nome")]
    public string Nome { get; set; } = string.Empty;

    [Column("imagemurl")]
    public string ImagemUrl { get; set; }

    public ICollection<CategoriaProduto> CategoriasProdutos { get; set; } = new List<CategoriaProduto>();
}