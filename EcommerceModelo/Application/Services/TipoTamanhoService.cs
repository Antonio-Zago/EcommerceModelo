using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class TipoTamanhoService : BaseService<TipoTamanho>, ITipoTamanhoService
{
    public TipoTamanhoService(ITipoTamanhoRepository repository) : base(repository) { }
}
