using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class ProdutoImagem
{
    [Column("id")]
    public int Id { get; set; }

    [Column("imagemurl")]
    public string ImagemUrl { get; set; } = string.Empty;

    [Column("produto")]
    public int ProdutoId { get; set; }
    public Produto Produto { get; set; } = null!;

    [Column("principal")]
    public bool Principal { get; set; }
}
