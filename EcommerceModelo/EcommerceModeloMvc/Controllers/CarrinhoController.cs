using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceModeloMvc.Controllers;

[Authorize]
public class CarrinhoController : Controller
{
    private readonly ICarrinhoService _carrinhoService;

    public CarrinhoController(ICarrinhoService carrinhoService)
    {
        _carrinhoService = carrinhoService;
    }

    public async Task<IActionResult> Index()
    {
        var carrinho = await _carrinhoService.ObterCarrinhoAsync(UsuarioId());
        return View(carrinho);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Adicionar(int produtoId, string nome, decimal preco,
        string? imagemUrl, int? tamanhoId, string? tamanhoNome, int quantidade = 1)
    {
        if (quantidade <= 0)
            quantidade = 1;

        await _carrinhoService.AdicionarItemAsync(UsuarioId(), new CarrinhoItem
        {
            ProdutoId = produtoId,
            Nome = nome,
            Preco = preco,
            ImagemUrl = imagemUrl,
            TamanhoId = tamanhoId,
            TamanhoNome = tamanhoNome,
            Quantidade = quantidade
        });

        TempData["Sucesso"] = $"\"{nome}\" adicionado ao carrinho.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Remover(int produtoId, int? tamanhoId)
    {
        await _carrinhoService.RemoverItemAsync(UsuarioId(), produtoId, tamanhoId);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AtualizarQuantidade(int produtoId, int? tamanhoId, int quantidade)
    {
        await _carrinhoService.AtualizarQuantidadeAsync(UsuarioId(), produtoId, tamanhoId, quantidade);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Limpar()
    {
        await _carrinhoService.LimparCarrinhoAsync(UsuarioId());
        return RedirectToAction(nameof(Index));
    }

    private int UsuarioId() =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
}
