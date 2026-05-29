using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class CompraItemService : BaseService<CompraItem>, ICompraItemService
{
    public CompraItemService(ICompraItemRepository repository) : base(repository) { }
}
