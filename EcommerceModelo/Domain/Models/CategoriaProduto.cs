using System.ComponentModel.DataAnnotations.Schema;

public class CategoriaProduto
{
    [Column("produto")]
    public int ProdutoId { get; set; }
    public Produto Produto { get; set; } = null!;

    [Column("categoria")]
    public int CategoriaId { get; set; }
    public Categoria Categoria { get; set; } = null!;
}