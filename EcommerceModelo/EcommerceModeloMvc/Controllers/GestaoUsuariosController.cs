using Application.Interfaces;
using EcommerceModeloMvc.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceModeloMvc.Controllers;

[Authorize(Roles = "Admin")]
public class GestaoUsuariosController : Controller
{
    private readonly IUsuarioService _usuarioService;
    private readonly IPapelService _papelService;

    public GestaoUsuariosController(IUsuarioService usuarioService, IPapelService papelService)
    {
        _usuarioService = usuarioService;
        _papelService = papelService;
    }

    public async Task<IActionResult> Index()
    {
        var viewModel = new GestaoUsuariosViewModel
        {
            Usuarios = await _usuarioService.ObterTodosComPapeisAsync(),
            Papeis = await _papelService.ObterTodosAsync()
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AtribuirPapel(int usuarioId, int papelId)
    {
        await _usuarioService.AtribuirPapelAsync(usuarioId, papelId);
        TempData["Sucesso"] = "Papel atribuído com sucesso.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RemoverPapel(int usuarioId, int papelId)
    {
        await _usuarioService.RemoverPapelAsync(usuarioId, papelId);
        TempData["Sucesso"] = "Papel removido com sucesso.";
        return RedirectToAction(nameof(Index));
    }
}
