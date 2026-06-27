using Domain.Models;

namespace Domain.Interfaces;

public interface ICompraRepository : IBaseRepository<Compra>
{
    Task<Compra?> ObterComItensAsync(int id);
}
