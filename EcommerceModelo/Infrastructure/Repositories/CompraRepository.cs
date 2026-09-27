using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CompraRepository : BaseRepository<Compra>, ICompraRepository
{
    public CompraRepository(AppDbContext context) : base(context) { }

    public async Task AdicionarComBaixaDeEstoqueAsync(Compra compra)
    {
        await using var transacao = await _context.Database.BeginTransactionAsync();

        var solicitados = compra.Itens
            .GroupBy(i => (i.ProdutoId, i.TamanhoId))
            .Select(g => (g.Key.ProdutoId, g.Key.TamanhoId, Quantidade: g.Sum(i => i.Quantidade)));

        foreach (var (produtoId, tamanhoId, quantidade) in solicitados)
        {
            var baixados = await _context.ProdutoEstoques
                .Where(e => e.ProdutoId == produtoId && e.TamanhoId == tamanhoId && e.Quantidade >= quantidade)
                .ExecuteUpdateAsync(s => s.SetProperty(e => e.Quantidade, e => e.Quantidade - quantidade));

            if (baixados == 0)
                throw new InvalidOperationException("Um ou mais itens ficaram sem estoque suficiente durante a confirmação do pedido.");
        }

        await _dbSet.AddAsync(compra);
        await _context.SaveChangesAsync();
        await transacao.CommitAsync();
    }

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

    public async Task<bool> UsuarioComprouProdutoAsync(int usuarioId, int produtoId)
        => await _context.CompraItens
            .AnyAsync(i => i.ProdutoId == produtoId
                && i.Compra.UsuarioId == usuarioId
                && i.Compra.Status != "cancelado");
}
