using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class TipoTamanho
{
    [Column("id")]
    public int Id { get; set; }

    [Column("nome")]
    public string Nome { get; set; } = string.Empty;

    public ICollection<OpcaoTamanho> Opcoes { get; set; } = new List<OpcaoTamanho>();
}
