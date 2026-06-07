using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class OpcaoTamanhoService : BaseService<OpcaoTamanho>, IOpcaoTamanhoService
{
    private readonly IOpcaoTamanhoRepository _opcaoRepository;

    public OpcaoTamanhoService(IOpcaoTamanhoRepository repository) : base(repository)
    {
        _opcaoRepository = repository;
    }

    public Task<IEnumerable<OpcaoTamanho>> ObterTodosComTipoAsync()
        => _opcaoRepository.ObterTodosComTipoAsync();
}
