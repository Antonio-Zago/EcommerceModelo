using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Context;

namespace Infrastructure.Repositories;

public class TipoTamanhoRepository : BaseRepository<TipoTamanho>, ITipoTamanhoRepository
{
    public TipoTamanhoRepository(AppDbContext context) : base(context) { }
}
