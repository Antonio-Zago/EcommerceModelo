using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class CompraItemService : BaseService<CompraItem>, ICompraItemService
{
    private readonly ICompraItemRepository _compraItemRepository;

    public CompraItemService(ICompraItemRepository repository) : base(repository)
    {
        _compraItemRepository = repository;
    }

    public Task<IEnumerable<int>> ObterIdsProdutosMaisVendidosAsync()
        => _compraItemRepository.ObterIdsProdutosMaisVendidosAsync();
}
