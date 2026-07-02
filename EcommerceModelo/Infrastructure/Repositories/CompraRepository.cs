using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CompraRepository : BaseRepository<Compra>, ICompraRepository
{
    public CompraRepository(AppDbContext context) : base(context) { }

    public async Task<Compra?> ObterComItensAsync(int id)
        => await _context.Compras
            .Include(c => c.Itens)
            .FirstOrDefaultAsync(c => c.Id == id);

    public async Task<IEnumerable<Compra>> ObterPorUsuarioAsync(int usuarioId)
        => await _context.Compras
            .Include(c => c.Itens)
            .Where(c => c.UsuarioId == usuarioId)
            .OrderByDescending(c => c.CriadoEm)
            .ToListAsync();
}
