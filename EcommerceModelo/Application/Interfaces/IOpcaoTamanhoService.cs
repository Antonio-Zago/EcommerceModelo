using Domain.Models;

namespace Application.Interfaces;

public interface IOpcaoTamanhoService : IBaseService<OpcaoTamanho>
{
    Task<IEnumerable<OpcaoTamanho>> ObterTodosComTipoAsync();
}
