using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class ProdutoEstoque
{
    [Column("id")]
    public int Id { get; set; }

    [Column("quantidade")]
    public int Quantidade { get; set; }

    [Column("tamanho")]
    public int? TamanhoId { get; set; }
    public OpcaoTamanho? Tamanho { get; set; }

    [Column("produto")]
    public int ProdutoId { get; set; }
    public Produto Produto { get; set; } = null!;
}
