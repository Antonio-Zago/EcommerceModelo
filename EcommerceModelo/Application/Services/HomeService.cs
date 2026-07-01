using Application.Dtos.HomePage;
using Application.Interfaces;
using Domain.Models;

namespace Application.Services;

public class HomeService : IHomeService
{
    private readonly ICategoriaService _categoriaService;
    private readonly IProdutoService _produtoService;
    private readonly ICompraItemService _compraItemService;

    public HomeService(
        ICategoriaService categoriaService,
        IProdutoService produtoService,
        ICompraItemService compraItemService)
    {
        _categoriaService = categoriaService;
        _produtoService = produtoService;
        _compraItemService = compraItemService;
    }

    public async Task<HomeDto> ObterHomeDtoAsync()
    {
        var (categorias, todos, idsMaisVendidos) = (
            await _categoriaService.ObterTodosAsync(),
            await _produtoService.ObterTodosComImagensAsync(),
            await _compraItemService.ObterIdsProdutosMaisVendidosAsync()
        );

        var categoriasDtos = categorias.Select(c => new CategoriaDto
        {
            Nome = c.Nome,
            ImagemBase64 = c.ImagemUrl
        }).ToList();

        static ProdutoDto ToDto(Produto p) => new ProdutoDto
        {
            Id = p.Id,
            Nome = p.Nome,
            Preco = p.Preco,
            Descricao = p.Descricao,
            ImagemPrincipalUrl = p.Imagens.FirstOrDefault(i => i.Principal)?.ImagemUrl ?? string.Empty
        };

        var todosDtos = todos.Select(ToDto).ToList();

        var ordemVendas = idsMaisVendidos
            .Select((id, idx) => (id, idx))
            .ToDictionary(x => x.id, x => x.idx);

        var maisVendidosDtos = todosDtos
            .OrderBy(p => ordemVendas.TryGetValue(p.Id, out var pos) ? pos : int.MaxValue)
            .ToList();

        var umMesAtras = DateTime.UtcNow.AddMonths(-1);
        var novidadesDtos = todos
            .Where(p => p.DataCadastro >= umMesAtras)
            .OrderByDescending(p => p.DataCadastro)
            .Select(ToDto)
            .ToList();

        return new HomeDto
        {
            Categorias = categoriasDtos,
            MaisVendidos = maisVendidosDtos,
            Novidades = novidadesDtos,
            Recomendados = todosDtos
        };
    }
}
