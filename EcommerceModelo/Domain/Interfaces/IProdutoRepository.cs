using Domain.Models;

namespace Domain.Interfaces;

public interface IProdutoRepository : IBaseRepository<Produto>
{
    Task<IEnumerable<Produto>> ObterTodosComImagensAsync();
}
