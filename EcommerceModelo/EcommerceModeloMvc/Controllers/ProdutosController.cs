using Application.Interfaces;
using Domain.Models;
using EcommerceModeloMvc.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceModeloMvc.Controllers;

public class ProdutosController : Controller
{
    private readonly IProdutoService _produtoService;
    private readonly ICategoriaService _categoriaService;
    private readonly IWebHostEnvironment _env;

    private static readonly string[] _extensoesPermitidas = [".jpg", ".jpeg", ".png", ".webp"];
    private const long _tamanhoMaximoPorArquivo = 5 * 1024 * 1024; // 5 MB

    public ProdutosController(IProdutoService produtoService, ICategoriaService categoriaService, IWebHostEnvironment env)
    {
        _produtoService = produtoService;
        _categoriaService = categoriaService;
        _env = env;
    }

    public async Task<IActionResult> Cadastrar()
    {
        ViewBag.Categorias = await _categoriaService.ObterTodosAsync();
        return View(new CadastroProdutoViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cadastrar(CadastroProdutoViewModel viewModel)
    {
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
            ViewBag.Categorias = await _categoriaService.ObterTodosAsync();
            return View(viewModel);
        }

        try
        {
            var produto = new Produto
            {
                Nome = viewModel.Nome,
                Preco = viewModel.Preco,
                Descricao = viewModel.Descricao,
                QtdEstoque = viewModel.QtdEstoque,
                Tamanho = viewModel.Tamanho,
                CategoriasProdutos = viewModel.CategoriaIds
                    .Select(id => new CategoriaProduto { CategoriaId = id })
                    .ToList()
            };

            var pastaFisica = Path.Combine(_env.WebRootPath, "images", "produtos");

            var imagens = viewModel.Imagens!
                .Select(f => (f.OpenReadStream(), f.FileName))
                .ToList();

            await _produtoService.CadastrarComImagensAsync(produto, imagens, viewModel.ImagemPrincipalIndex, pastaFisica);

            TempData["Sucesso"] = $"Produto \"{produto.Nome}\" cadastrado com sucesso!";
            return RedirectToAction(nameof(Cadastrar));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Erro ao cadastrar produto: {ex.Message}");
            ViewBag.Categorias = await _categoriaService.ObterTodosAsync();
            return View(viewModel);
        }
    }
}
