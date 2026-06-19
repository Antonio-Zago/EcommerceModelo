using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class PapelService : BaseService<Papel>, IPapelService
{
    private readonly IPapelRepository _papelRepository;

    public PapelService(IPapelRepository papelRepository) : base(papelRepository)
    {
        _papelRepository = papelRepository;
    }

    public Task<Papel?> ObterPorNomeAsync(string nome)
        => _papelRepository.ObterPorNomeAsync(nome);
}
