using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class PapelUsuario
{
    [Column("usuario")]
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;

    [Column("papel")]
    public int PapelId { get; set; }
    public Papel Papel { get; set; } = null!;
}
