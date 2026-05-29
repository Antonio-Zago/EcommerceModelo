using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Context;

namespace Infrastructure.Repositories;

public class ProdutoImagemRepository : BaseRepository<ProdutoImagem>, IProdutoImagemRepository
{
    public ProdutoImagemRepository(AppDbContext context) : base(context) { }
}
