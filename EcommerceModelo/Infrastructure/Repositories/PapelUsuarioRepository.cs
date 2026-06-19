using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PapelUsuarioRepository : IPapelUsuarioRepository
{
    private readonly AppDbContext _context;

    public PapelUsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExisteAsync(int usuarioId, int papelId)
        => await _context.PapeisUsuario.AnyAsync(pu => pu.UsuarioId == usuarioId && pu.PapelId == papelId);

    public async Task AdicionarAsync(PapelUsuario papelUsuario)
    {
        _context.PapeisUsuario.Add(papelUsuario);
        await _context.SaveChangesAsync();
    }

    public async Task RemoverAsync(int usuarioId, int papelId)
    {
        var registro = await _context.PapeisUsuario
            .FirstOrDefaultAsync(pu => pu.UsuarioId == usuarioId && pu.PapelId == papelId);

        if (registro is not null)
        {
            _context.PapeisUsuario.Remove(registro);
            await _context.SaveChangesAsync();
        }
    }
}
