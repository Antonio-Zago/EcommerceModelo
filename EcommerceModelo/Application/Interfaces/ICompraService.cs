using Domain.Models;

namespace Application.Interfaces;

public interface ICompraService : IBaseService<Compra>
{
    Task<IEnumerable<Compra>> ObterPorUsuarioAsync(int usuarioId);
}
