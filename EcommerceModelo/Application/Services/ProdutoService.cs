using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class ProdutoService : BaseService<Produto>, IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoService(IProdutoRepository repository) : base(repository)
    {
        _produtoRepository = repository;
    }

    public Task<IEnumerable<Produto>> ObterTodosComImagensAsync()
        => _produtoRepository.ObterTodosComImagensAsync();
}
