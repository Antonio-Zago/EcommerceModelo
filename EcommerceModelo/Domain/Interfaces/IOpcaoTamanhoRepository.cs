using Domain.Models;

namespace Domain.Interfaces;

public interface IOpcaoTamanhoRepository : IBaseRepository<OpcaoTamanho>
{
    Task<IEnumerable<OpcaoTamanho>> ObterTodosComTipoAsync();
}
