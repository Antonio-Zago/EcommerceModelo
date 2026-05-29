using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class Categoria
{
    [Column("id")]
    public int Id { get; set; }

    [Column("nome")]
    public string Nome { get; set; } = string.Empty;

    [Column("imagemurl")]
    public string ImagemUrl { get; set; } = string.Empty;

    public ICollection<CategoriaProduto> CategoriasProdutos { get; set; } = new List<CategoriaProduto>();
}
