using Application.Interfaces;
using Domain.Interfaces;

namespace Application.Services;

public class BaseService<T> : IBaseService<T> where T : class
{
    protected readonly IBaseRepository<T> _repository;

    public BaseService(IBaseRepository<T> repository)
    {
        _repository = repository;
    }

    public Task<T?> ObterPorIdAsync(int id)
        => _repository.ObterPorIdAsync(id);

    public Task<IEnumerable<T>> ObterTodosAsync()
        => _repository.ObterTodosAsync();

    public Task AdicionarAsync(T entidade)
        => _repository.AdicionarAsync(entidade);

    public Task AtualizarAsync(T entidade)
        => _repository.AtualizarAsync(entidade);

    public Task RemoverAsync(int id)
        => _repository.RemoverAsync(id);
}
