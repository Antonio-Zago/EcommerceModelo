using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CompraItemRepository : BaseRepository<CompraItem>, ICompraItemRepository
{
    public CompraItemRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<int>> ObterIdsProdutosMaisVendidosAsync()
        => await _context.CompraItens
            .GroupBy(i => i.ProdutoId)
            .OrderByDescending(g => g.Sum(i => i.Quantidade))
            .Select(g => g.Key)
            .ToListAsync();
}
