using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class Compra
{
    [Column("id")]
    public int Id { get; set; }

    public ICollection<CompraItem> Itens { get; set; } = new List<CompraItem>();
}
