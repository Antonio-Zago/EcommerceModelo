using Domain.Enums;
using Domain.Models;

namespace Domain.Interfaces;

public interface IProdutoRepository : IBaseRepository<Produto>
{
    Task<IEnumerable<Produto>> ObterTodosComImagensAsync();
    Task<IEnumerable<Produto>> ObterPorGeneroComImagensAsync(Genero genero);
    Task<IEnumerable<Produto>> ObterInfantisComImagensAsync();
}
