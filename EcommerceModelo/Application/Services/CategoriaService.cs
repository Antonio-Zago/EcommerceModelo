using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class CategoriaService : BaseService<Categoria>, ICategoriaService
{
    public CategoriaService(ICategoriaRepository repository) : base(repository) { }
}
