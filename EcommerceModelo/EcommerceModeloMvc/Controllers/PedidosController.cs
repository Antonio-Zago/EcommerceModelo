using Application.Dtos.Avaliacao;
using Application.Interfaces;
using EcommerceModeloMvc.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceModeloMvc.Controllers;

[Authorize]
public class PedidosController : Controller
{
    private readonly ICompraService _compraService;
    private readonly IProdutoAvaliacaoService _avaliacaoService;

    public PedidosController(ICompraService compraService, IProdutoAvaliacaoService avaliacaoService)
    {
        _compraService = compraService;
        _avaliacaoService = avaliacaoService;
    }

    public async Task<IActionResult> Index()
    {
        var pedidos = await _compraService.ObterPorUsuarioAsync(UsuarioId());
        var avaliacoes = await _avaliacaoService.ObterPorUsuarioAsync(UsuarioId());

        return View(new PedidosViewModel
        {
            Pedidos = pedidos,
            Avaliacoes = avaliacoes.ToDictionary(a => a.ProdutoId)
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Avaliar(AvaliarProdutoDto avaliacao)
    {
        if (!ModelState.IsValid)
        {
            TempData["Erro"] = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .FirstOrDefault(m => !string.IsNullOrWhiteSpace(m)) ?? "Avaliação inválida.";
            return RedirectToAction(nameof(Index));
        }

        try
        {
            await _avaliacaoService.AvaliarAsync(UsuarioId(), avaliacao);
            TempData["Sucesso"] = "Obrigado! Sua avaliação foi registrada.";
        }
        catch (InvalidOperationException ex)
        {
            TempData["Erro"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }

    private int UsuarioId() =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
}
