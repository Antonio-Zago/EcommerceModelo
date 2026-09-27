using Domain.Models;

namespace Domain.Interfaces;

public interface IProdutoEstoqueRepository : IBaseRepository<ProdutoEstoque>
{
    Task<IEnumerable<ProdutoEstoque>> ObterPorProdutosAsync(IEnumerable<int> produtoIds);
}
