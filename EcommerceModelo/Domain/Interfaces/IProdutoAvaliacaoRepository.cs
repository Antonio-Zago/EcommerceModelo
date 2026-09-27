using Domain.Models;

namespace Domain.Interfaces;

public interface IProdutoAvaliacaoRepository : IBaseRepository<ProdutoAvaliacao>
{
    Task<IEnumerable<ProdutoAvaliacao>> ObterPorUsuarioAsync(int usuarioId);
    Task<ProdutoAvaliacao?> ObterPorUsuarioEProdutoAsync(int usuarioId, int produtoId);
}
