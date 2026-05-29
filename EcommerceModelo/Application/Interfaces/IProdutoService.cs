using Domain.Models;

namespace Application.Interfaces;

public interface IProdutoService : IBaseService<Produto>
{
    Task<IEnumerable<Produto>> ObterTodosComImagensAsync();
}
