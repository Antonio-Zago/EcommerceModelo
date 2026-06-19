using Domain.Models;

namespace Domain.Interfaces;

public interface IPapelUsuarioRepository
{
    Task<bool> ExisteAsync(int usuarioId, int papelId);
    Task AdicionarAsync(PapelUsuario papelUsuario);
    Task RemoverAsync(int usuarioId, int papelId);
}
