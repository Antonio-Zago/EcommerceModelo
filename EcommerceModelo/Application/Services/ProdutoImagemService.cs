using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class ProdutoImagemService : BaseService<ProdutoImagem>, IProdutoImagemService
{
    public ProdutoImagemService(IProdutoImagemRepository repository) : base(repository) { }
}
