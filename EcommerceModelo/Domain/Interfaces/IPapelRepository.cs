using Domain.Models;

namespace Domain.Interfaces;

public interface IPapelRepository : IBaseRepository<Papel>
{
    Task<Papel?> ObterPorNomeAsync(string nome);
}
