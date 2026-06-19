using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PapelRepository : BaseRepository<Papel>, IPapelRepository
{
    public PapelRepository(AppDbContext context) : base(context) { }

    public async Task<Papel?> ObterPorNomeAsync(string nome)
        => await _dbSet.FirstOrDefaultAsync(p => p.Nome == nome);
}
