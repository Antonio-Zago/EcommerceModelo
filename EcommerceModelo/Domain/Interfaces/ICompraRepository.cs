using Domain.Models;

namespace Domain.Interfaces;

public interface ICompraRepository : IBaseRepository<Compra>
{
    Task AdicionarComBaixaDeEstoqueAsync(Compra compra);
    Task<Compra?> ObterComItensAsync(int id);
    Task<IEnumerable<Compra>> ObterPorUsuarioAsync(int usuarioId);
}
