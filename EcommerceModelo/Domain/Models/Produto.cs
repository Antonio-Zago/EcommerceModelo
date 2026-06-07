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

    [Column("categoria")]
    public int CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }

    public ICollection<ProdutoImagem> Imagens { get; set; } = new List<ProdutoImagem>();

    public ICollection<ProdutoEstoque> Estoques { get; set; } = new List<ProdutoEstoque>();
}
