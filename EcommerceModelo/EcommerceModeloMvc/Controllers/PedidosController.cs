using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceModeloMvc.Controllers;

[Authorize]
public class PedidosController : Controller
{
    private readonly ICompraService _compraService;

    public PedidosController(ICompraService compraService)
    {
        _compraService = compraService;
    }

    public async Task<IActionResult> Index()
    {
        var pedidos = await _compraService.ObterPorUsuarioAsync(UsuarioId());
        return View(pedidos);
    }

    private int UsuarioId() =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
}
