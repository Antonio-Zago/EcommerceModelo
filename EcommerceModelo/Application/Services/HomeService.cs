using Application.Dtos.HomePage;
using Application.Interfaces;

namespace Application.Services;

public class HomeService : IHomeService
{
    private readonly ICategoriaService _categoriaService;
    private readonly IProdutoService _produtoService;

    public HomeService(ICategoriaService categoriaService, IProdutoService produtoService)
    {
        _categoriaService = categoriaService;
        _produtoService = produtoService;
    }

    public async Task<HomeDto> ObterHomeDtoAsync()
    {
        var categorias = await _categoriaService.ObterTodosAsync();
        var produtos = await _produtoService.ObterTodosComImagensAsync();

        var categoriasDtos = categorias.Select(c => new CategoriaDto
        {
            Nome = c.Nome,
            ImagemBase64 = c.ImagemUrl
        }).ToList();

        var listaProdutosDtos = new List<ProdutoDto>();

        foreach (var p in produtos)
        {
            var imagemPrincipal = p.Imagens.FirstOrDefault(i => i.Principal)?.ImagemUrl ?? string.Empty;

            var produtoDto = new ProdutoDto
            {
                Nome = p.Nome,
                Preco = p.Preco,
                Descricao = p.Descricao,
                ImagemPrincipalUrl = imagemPrincipal
            };

            listaProdutosDtos.Add(produtoDto);
        }

        return new HomeDto
        {
            Categorias = categoriasDtos,
            MaisVendidos = listaProdutosDtos,
            Novidades = listaProdutosDtos,
            Recomendados = listaProdutosDtos
        };
    }
}
