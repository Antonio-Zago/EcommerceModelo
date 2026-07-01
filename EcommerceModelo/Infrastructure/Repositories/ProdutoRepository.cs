using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Produto>> ObterTodosComImagensAsync()
        => await _dbSet.Include(p => p.Imagens).ToListAsync();

    public async Task<IEnumerable<Produto>> ObterPorGeneroComImagensAsync(Genero genero)
        => await _dbSet
            .Where(p => p.Genero == genero && !p.EhInfantil)
            .Include(p => p.Imagens)
            .ToListAsync();

    public async Task<IEnumerable<Produto>> ObterInfantisComImagensAsync()
        => await _dbSet
            .Where(p => p.EhInfantil)
            .Include(p => p.Imagens)
            .ToListAsync();

    public async Task<Produto?> ObterPorIdComDetalhesAsync(int id)
        => await _dbSet
            .Include(p => p.Imagens)
            .Include(p => p.Estoques).ThenInclude(e => e.Tamanho)
            .Include(p => p.Categoria)
            .FirstOrDefaultAsync(p => p.Id == id);
}
