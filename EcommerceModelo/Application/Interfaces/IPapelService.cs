using Domain.Models;

namespace Application.Interfaces;

public interface IPapelService : IBaseService<Papel>
{
    Task<Papel?> ObterPorNomeAsync(string nome);
}
