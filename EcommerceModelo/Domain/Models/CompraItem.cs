using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class CompraItem
{

    [Column("compra")]
    public int CompraId { get; set; }
    public Compra Compra { get; set; } = null!;

    [Column("produto")]
    public int ProdutoId { get; set; }

    public Produto Produto { get; set; } = null!;

    [Column("nome_produto")]
    public string NomeProduto { get; set; } = string.Empty;

    [Column("preco_unitario")]
    public decimal PrecoUnitario { get; set; }

    [Column("quantidade")]
    public int Quantidade { get; set; }

    [Column("tamanho_id")]
    public int? TamanhoId { get; set; }
}
