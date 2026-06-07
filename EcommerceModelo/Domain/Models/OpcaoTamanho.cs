using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class OpcaoTamanho
{
    [Column("id")]
    public int Id { get; set; }

    [Column("descricao")]
    public string Descricao { get; set; } = string.Empty;

    [Column("tipo")]
    public int TipoId { get; set; }
    public TipoTamanho Tipo { get; set; } = null!;

    public ICollection<ProdutoEstoque> Estoques { get; set; } = new List<ProdutoEstoque>();
}
