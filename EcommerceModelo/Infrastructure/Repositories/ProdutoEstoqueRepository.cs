using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Context;

namespace Infrastructure.Repositories;

public class ProdutoEstoqueRepository : BaseRepository<ProdutoEstoque>, IProdutoEstoqueRepository
{
    public ProdutoEstoqueRepository(AppDbContext context) : base(context) { }
}
