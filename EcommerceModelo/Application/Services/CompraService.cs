using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class CompraService : BaseService<Compra>, ICompraService
{
    private readonly ICompraRepository _compraRepository;

    public CompraService(ICompraRepository repository) : base(repository)
    {
        _compraRepository = repository;
    }

    public Task<IEnumerable<Compra>> ObterPorUsuarioAsync(int usuarioId)
        => _compraRepository.ObterPorUsuarioAsync(usuarioId);
}
