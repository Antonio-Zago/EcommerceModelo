using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class CompraService : BaseService<Compra>, ICompraService
{
    public CompraService(ICompraRepository repository) : base(repository) { }
}
