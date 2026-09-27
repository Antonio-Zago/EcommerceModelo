using Application.Dtos.Avaliacao;
using Domain.Models;

namespace Application.Interfaces;

public interface IProdutoAvaliacaoService : IBaseService<ProdutoAvaliacao>
{
    Task<IEnumerable<ProdutoAvaliacao>> ObterPorUsuarioAsync(int usuarioId);
    Task AvaliarAsync(int usuarioId, AvaliarProdutoDto avaliacao);
}
