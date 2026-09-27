using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProdutoAvaliacaoRepository : BaseRepository<ProdutoAvaliacao>, IProdutoAvaliacaoRepository
{
    public ProdutoAvaliacaoRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<ProdutoAvaliacao>> ObterPorUsuarioAsync(int usuarioId)
        => await _dbSet
            .AsNoTracking()
            .Where(a => a.UsuarioId == usuarioId)
            .ToListAsync();

    public async Task<IEnumerable<ProdutoAvaliacao>> ObterPorProdutoAsync(int produtoId)
        => await _dbSet
            .AsNoTracking()
            .Include(a => a.Usuario)
            .Where(a => a.ProdutoId == produtoId)
            .OrderByDescending(a => a.CriadoEm)
            .ToListAsync();

    public async Task<ProdutoAvaliacao?> ObterPorUsuarioEProdutoAsync(int usuarioId, int produtoId)
        => await _dbSet
            .FirstOrDefaultAsync(a => a.UsuarioId == usuarioId && a.ProdutoId == produtoId);
}
