using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(AppDbContext context) : base(context) { }

    public async Task<Usuario?> ObterPorEmailAsync(string email)
        => await _dbSet
            .Include(u => u.PapeisUsuario)
                .ThenInclude(pu => pu.Papel)
            .FirstOrDefaultAsync(u => u.Email == email);

    public async Task<bool> EmailExisteAsync(string email)
        => await _dbSet.AnyAsync(u => u.Email == email);

    public async Task<IEnumerable<Usuario>> ObterTodosComPapeisAsync()
        => await _dbSet
            .Include(u => u.PapeisUsuario)
                .ThenInclude(pu => pu.Papel)
            .OrderBy(u => u.Nome)
            .ToListAsync();
}
