using Application.Interfaces;
using Domain.Models;
using EcommerceModeloMvc.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceModeloMvc.Controllers;

[Authorize(Roles = "Admin")]
public class TamanhosController : Controller
{
    private readonly ITipoTamanhoService _tipoTamanhoService;
    private readonly IOpcaoTamanhoService _opcaoTamanhoService;

    public TamanhosController(ITipoTamanhoService tipoTamanhoService, IOpcaoTamanhoService opcaoTamanhoService)
    {
        _tipoTamanhoService = tipoTamanhoService;
        _opcaoTamanhoService = opcaoTamanhoService;
    }

    // GET: /Tamanhos/Cadastrar?tab=tipos|opcoes
    public async Task<IActionResult> Cadastrar(string tab = "tipos")
    {
        await PopularViewBagAsync();
        ViewBag.TabAtiva = tab;
        return View(new CadastroTipoTamanhoViewModel());
    }

    // POST: /Tamanhos/CadastrarTipo
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CadastrarTipo(CadastroTipoTamanhoViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            await PopularViewBagAsync();
            ViewBag.TabAtiva = "tipos";
            return View("Cadastrar", viewModel);
        }

        try
        {
            await _tipoTamanhoService.AdicionarAsync(new TipoTamanho { Nome = viewModel.Nome });
            TempData["Sucesso"] = $"Tipo de tamanho \"{viewModel.Nome}\" cadastrado com sucesso.";
            TempData["TabAtiva"] = "tipos";
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Erro ao cadastrar tipo: {ex.Message}");
            await PopularViewBagAsync();
            ViewBag.TabAtiva = "tipos";
            return View("Cadastrar", viewModel);
        }

        return RedirectToAction(nameof(Cadastrar), new { tab = "tipos" });
    }

    // POST: /Tamanhos/CadastrarOpcao
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CadastrarOpcao(CadastroOpcaoTamanhoViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            await PopularViewBagAsync();
            ViewBag.TabAtiva = "opcoes";
            ViewBag.OpcaoViewModel = viewModel;
            return View("Cadastrar", new CadastroTipoTamanhoViewModel());
        }

        try
        {
            await _opcaoTamanhoService.AdicionarAsync(new OpcaoTamanho
            {
                Descricao = viewModel.Descricao,
                TipoId    = viewModel.TipoId
            });
            TempData["Sucesso"] = $"Opção de tamanho \"{viewModel.Descricao}\" cadastrada com sucesso.";
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Erro ao cadastrar opção: {ex.Message}");
            await PopularViewBagAsync();
            ViewBag.TabAtiva = "opcoes";
            ViewBag.OpcaoViewModel = viewModel;
            return View("Cadastrar", new CadastroTipoTamanhoViewModel());
        }

        return RedirectToAction(nameof(Cadastrar), new { tab = "opcoes" });
    }

    private async Task PopularViewBagAsync()
    {
        ViewBag.Tipos  = await _tipoTamanhoService.ObterTodosAsync();
        ViewBag.Opcoes = await _opcaoTamanhoService.ObterTodosComTipoAsync();
    }
}
