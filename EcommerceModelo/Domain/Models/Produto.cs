using System.ComponentModel.DataAnnotations.Schema;

public class Produto
{
    [Column("id")]
    public int Id { get; set; }

    [Column("nome")]
    public string Nome { get; set; } = string.Empty;

    [Column("preco")]
    public decimal Preco { get; set; }

    [Column("descricao")]
    public string Descricao { get; set; } = string.Empty;

    [Column("qtdestoque")]
    public int QtdEstoque { get; set; }

    [Column("tamanho")]
    public string Tamanho { get; set; }

    public ICollection<CategoriaProduto> CategoriasProdutos { get; set; } = new List<CategoriaProduto>();
}