using Application.Dtos.Checkout;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;
using EcommerceModeloMvc.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceModeloMvc.Controllers;

[Authorize]
public class CheckoutController : Controller
{
    private readonly ICarrinhoService _carrinhoService;
    private readonly ICheckoutService _checkoutService;
    private readonly IViaCepService _viaCepService;
    private readonly ICompraRepository _compraRepository;

    public CheckoutController(
        ICarrinhoService carrinhoService,
        ICheckoutService checkoutService,
        IViaCepService viaCepService,
        ICompraRepository compraRepository)
    {
        _carrinhoService = carrinhoService;
        _checkoutService = checkoutService;
        _viaCepService = viaCepService;
        _compraRepository = compraRepository;
    }

    public async Task<IActionResult> Index()
    {
        var carrinho = await _carrinhoService.ObterCarrinhoAsync(UsuarioId());

        if (!carrinho.Itens.Any())
            return RedirectToAction("Index", "Carrinho");

        var errosEstoque = await _checkoutService.VerificarEstoqueAsync(carrinho);
        if (errosEstoque.Count > 0)
            return RedirecionarCarrinhoComErros(errosEstoque);

        return View(new CheckoutViewModel { Carrinho = carrinho });
    }

    [HttpGet]
    public async Task<IActionResult> BuscarCep(string cep)
    {
        var endereco = await _viaCepService.BuscarPorCepAsync(cep);

        if (endereco is null)
            return NotFound(new { erro = "CEP não encontrado." });

        return Json(endereco);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Confirmar(ConfirmarPedidoDto pedido)
    {
        var carrinho = await _carrinhoService.ObterCarrinhoAsync(UsuarioId());

        if (!carrinho.Itens.Any())
            return RedirectToAction("Index", "Carrinho");

        var errosEstoque = await _checkoutService.VerificarEstoqueAsync(carrinho);
        if (errosEstoque.Count > 0)
            return RedirecionarCarrinhoComErros(errosEstoque);

        if (!ModelState.IsValid)
            return View("Index", new CheckoutViewModel { Carrinho = carrinho, Pedido = pedido });

        Compra compra;
        try
        {
            compra = await _checkoutService.ConfirmarPedidoAsync(UsuarioId(), pedido, carrinho);
        }
        catch (InvalidOperationException ex)
        {
            return RedirecionarCarrinhoComErros([ex.Message]);
        }

        await _carrinhoService.LimparCarrinhoAsync(UsuarioId());

        return RedirectToAction(nameof(Confirmado), new { id = compra.Id });
    }

    public async Task<IActionResult> Confirmado(int id)
    {
        var compra = await _compraRepository.ObterComItensAsync(id);

        if (compra is null)
            return NotFound();

        return View(compra);
    }

    private IActionResult RedirecionarCarrinhoComErros(IReadOnlyList<string> erros)
    {
        TempData["ErrosEstoque"] = erros.ToArray();
        return RedirectToAction("Index", "Carrinho");
    }

    private int UsuarioId() =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
}
