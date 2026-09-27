using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class ProdutoAvaliacao
{
    [Column("id")]
    public int Id { get; set; }

    [Column("produto")]
    public int ProdutoId { get; set; }
    public Produto Produto { get; set; } = null!;

    [Column("usuario")]
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;

    [Column("descricao")]
    public string? Descricao { get; set; }

    [Column("nota")]
    public int Nota { get; set; }

    [Column("criado_em")]
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
}
