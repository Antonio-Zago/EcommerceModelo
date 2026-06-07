using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class OpcaoTamanhoRepository : BaseRepository<OpcaoTamanho>, IOpcaoTamanhoRepository
{
    public OpcaoTamanhoRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<OpcaoTamanho>> ObterTodosComTipoAsync()
        => await _context.OpcaoTamanhos
            .Include(o => o.Tipo)
            .OrderBy(o => o.Tipo.Nome)
            .ThenBy(o => o.Descricao)
            .ToListAsync();
}
