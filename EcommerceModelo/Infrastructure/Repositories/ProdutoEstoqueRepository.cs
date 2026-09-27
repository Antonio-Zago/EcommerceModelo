using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProdutoEstoqueRepository : BaseRepository<ProdutoEstoque>, IProdutoEstoqueRepository
{
    public ProdutoEstoqueRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<ProdutoEstoque>> ObterPorProdutosAsync(IEnumerable<int> produtoIds)
    {
        var ids = produtoIds.Distinct().ToList();
        return await _dbSet
            .AsNoTracking()
            .Include(e => e.Produto)
            .Where(e => ids.Contains(e.ProdutoId))
            .ToListAsync();
    }
}
