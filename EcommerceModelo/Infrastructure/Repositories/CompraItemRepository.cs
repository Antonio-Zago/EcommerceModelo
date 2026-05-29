using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Context;

namespace Infrastructure.Repositories;

public class CompraItemRepository : BaseRepository<CompraItem>, ICompraItemRepository
{
    public CompraItemRepository(AppDbContext context) : base(context) { }
}
