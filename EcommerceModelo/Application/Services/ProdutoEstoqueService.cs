using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class ProdutoEstoqueService : BaseService<ProdutoEstoque>, IProdutoEstoqueService
{
    public ProdutoEstoqueService(IProdutoEstoqueRepository repository) : base(repository) { }
}
