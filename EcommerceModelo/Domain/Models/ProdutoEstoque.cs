using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class ProdutoEstoque
{
    [Column("id")]
    public int Id { get; set; }

    [Column("quantidade")]
    public int Quantidade { get; set; }

    [Column("tamanho")]
    public string Tamanho { get; set; } = string.Empty;

    [Column("produto")]
    public int ProdutoId { get; set; }
    public Produto Produto { get; set; } = null!;
}
