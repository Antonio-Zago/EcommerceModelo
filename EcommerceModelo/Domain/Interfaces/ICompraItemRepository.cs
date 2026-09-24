using Domain.Models;

namespace Domain.Interfaces;

public interface ICompraItemRepository : IBaseRepository<CompraItem>
{
    Task<IEnumerable<int>> ObterIdsProdutosMaisVendidosAsync();
    Task<bool> ProdutoJaVendidoAsync(int produtoId);
}
