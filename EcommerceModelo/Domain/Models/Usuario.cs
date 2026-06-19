using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class Usuario
{
    [Column("id")]
    public int Id { get; set; }

    [Column("nome")]
    public string Nome { get; set; } = string.Empty;

    [Column("email")]
    public string Email { get; set; } = string.Empty;

    [Column("senha_hash")]
    public string SenhaHash { get; set; } = string.Empty;

    public ICollection<PapelUsuario> PapeisUsuario { get; set; } = new List<PapelUsuario>();
}
