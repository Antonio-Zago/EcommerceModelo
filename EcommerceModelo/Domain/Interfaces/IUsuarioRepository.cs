using Domain.Models;

namespace Domain.Interfaces;

public interface IUsuarioRepository : IBaseRepository<Usuario>
{
    Task<Usuario?> ObterPorEmailAsync(string email);
    Task<bool> EmailExisteAsync(string email);
    Task<IEnumerable<Usuario>> ObterTodosComPapeisAsync();
}
