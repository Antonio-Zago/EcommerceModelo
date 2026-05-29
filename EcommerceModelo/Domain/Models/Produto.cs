using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

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
    public string Tamanho { get; set; } = string.Empty;

    public ICollection<CategoriaProduto> CategoriasProdutos { get; set; } = new List<CategoriaProduto>();

    public ICollection<ProdutoImagem> Imagens { get; set; } = new List<ProdutoImagem>();
}
