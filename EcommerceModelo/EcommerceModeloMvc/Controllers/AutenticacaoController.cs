using Application.Dtos.Auth;
using Application.Interfaces;
using EcommerceModeloMvc.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceModeloMvc.Controllers;

public class AutenticacaoController : Controller
{
    private readonly IUsuarioService _usuarioService;

    public AutenticacaoController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    // ── Cadastro ─────────────────────────────────────────────────────────────

    public IActionResult Cadastrar() => View(new CadastroUsuarioViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cadastrar(CadastroUsuarioViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        try
        {
            await _usuarioService.CadastrarAsync(new CadastroUsuarioDto
            {
                Nome = viewModel.Nome,
                Email = viewModel.Email,
                Senha = viewModel.Senha
            });

            TempData["Sucesso"] = "Conta criada com sucesso. Faça login para continuar.";
            return RedirectToAction(nameof(Login));
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(viewModel);
        }
    }

    // ── Login ─────────────────────────────────────────────────────────────────

    public IActionResult Login(string? returnUrl = null)
    {
        ViewBag.ReturnUrl = returnUrl;
        return View(new LoginViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel viewModel, string? returnUrl = null)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(viewModel);
        }

        var usuario = await _usuarioService.AutenticarAsync(new LoginDto
        {
            Email = viewModel.Email,
            Senha = viewModel.Senha
        });

        if (usuario is null)
        {
            ModelState.AddModelError(string.Empty, "E-mail ou senha inválidos.");
            ViewBag.ReturnUrl = returnUrl;
            return View(viewModel);
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new(ClaimTypes.Name, usuario.Nome),
            new(ClaimTypes.Email, usuario.Email)
        };

        foreach (var papelUsuario in usuario.PapeisUsuario)
            claims.Add(new Claim(ClaimTypes.Role, papelUsuario.Papel.Nome));

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        var authProperties = new AuthenticationProperties
        {
            IsPersistent = viewModel.Lembrar,
            ExpiresUtc = viewModel.Lembrar
                ? DateTimeOffset.UtcNow.AddDays(30)
                : DateTimeOffset.UtcNow.AddHours(8)
        };

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

        if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);

        return RedirectToAction("Index", "Home");
    }

    // ── Logout ────────────────────────────────────────────────────────────────

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}
