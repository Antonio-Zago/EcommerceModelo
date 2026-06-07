using Application.Interfaces;
using Domain.Models;
using EcommerceModeloMvc.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceModeloMvc.Controllers;

public class ProdutosController : Controller
{
    private readonly IProdutoService _produtoService;
    private readonly ICategoriaService _categoriaService;
    private readonly IOpcaoTamanhoService _opcaoTamanhoService;
    private readonly IWebHostEnvironment _env;

    private static readonly string[] _extensoesPermitidas = [".jpg", ".jpeg", ".png", ".webp"];
    private const long _tamanhoMaximoPorArquivo = 5 * 1024 * 1024; // 5 MB

    public ProdutosController(
        IProdutoService produtoService,
        ICategoriaService categoriaService,
        IOpcaoTamanhoService opcaoTamanhoService,
        IWebHostEnvironment env)
    {
        _produtoService = produtoService;
        _categoriaService = categoriaService;
        _opcaoTamanhoService = opcaoTamanhoService;
        _env = env;
    }

    public async Task<IActionResult> Cadastrar()
    {
        await PopularViewBagAsync();
        return View(new CadastroProdutoViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cadastrar(CadastroProdutoViewModel viewModel)
    {
        if (viewModel.Tamanhos == null || viewModel.Tamanhos.Count == 0)
            ModelState.AddModelError("Tamanhos", "Adicione ao menos um tamanho com quantidade.");
        else if (viewModel.Tamanhos.GroupBy(t => t.TamanhoId).Any(g => g.Count() > 1))
            ModelState.AddModelError("Tamanhos", "Não é permitido cadastrar o mesmo tamanho mais de uma vez.");

        if (viewModel.Imagens == null || viewModel.Imagens.Count == 0)
            ModelState.AddModelError("Imagens", "Adicione ao menos uma imagem do produto.");

        if (viewModel.Imagens != null)
        {
            foreach (var arquivo in viewModel.Imagens)
            {
                var ext = Path.GetExtension(arquivo.FileName).ToLowerInvariant();
                if (!_extensoesPermitidas.Contains(ext))
                    ModelState.AddModelError("Imagens", $"O arquivo \"{arquivo.FileName}\" possui formato não permitido. Use JPG, PNG ou WebP.");
                else if (arquivo.Length > _tamanhoMaximoPorArquivo)
                    ModelState.AddModelError("Imagens", $"O arquivo \"{arquivo.FileName}\" excede o tamanho máximo de 5 MB.");
            }
        }

        if (!ModelState.IsValid)
        {
            await PopularViewBagAsync();
            return View(viewModel);
        }

        try
        {
            var produto = new Produto
            {
                Nome = viewModel.Nome,
                Preco = viewModel.Preco,
                Descricao = viewModel.Descricao,
                CategoriaId = viewModel.CategoriaId
            };

            var estoques = viewModel.Tamanhos!
                .Select(t => (t.TamanhoId, t.QtdEstoque))
                .ToList();

            var imagens = viewModel.Imagens!
                .Select(f => (f.OpenReadStream(), f.FileName))
                .ToList();

            var pastaFisica = Path.Combine(_env.WebRootPath, "images", "produtos");

            await _produtoService.CadastrarComEstoqueAsync(produto, estoques, imagens, viewModel.ImagemPrincipalIndex, pastaFisica);

            TempData["Sucesso"] = $"Produto \"{viewModel.Nome}\" cadastrado com sucesso.";
            return RedirectToAction(nameof(Cadastrar));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Erro ao cadastrar produto: {ex.Message}");
            await PopularViewBagAsync();
            return View(viewModel);
        }
    }

    private async Task PopularViewBagAsync()
    {
        ViewBag.Categorias = await _categoriaService.ObterTodosAsync();
        ViewBag.OpcoesTamanho = await _opcaoTamanhoService.ObterTodosComTipoAsync();
    }
}
