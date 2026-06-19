using Application.Dtos.Auth;
using Domain.Models;

namespace Application.Interfaces;

public interface IUsuarioService
{
    Task<Usuario> CadastrarAsync(CadastroUsuarioDto dto);
    Task<Usuario?> AutenticarAsync(LoginDto dto);
    Task<IEnumerable<Usuario>> ObterTodosComPapeisAsync();
    Task AtribuirPapelAsync(int usuarioId, int papelId);
    Task RemoverPapelAsync(int usuarioId, int papelId);
}
