using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceModeloMvc.Controllers;

public class HomeController : Controller
{
    private readonly IHomeService _homeService;

    public HomeController(IHomeService homeService)
    {
        _homeService = homeService;
    }

    public async Task<IActionResult> Index()
    {
        var homeDto = await _homeService.ObterHomeDtoAsync();
        return View(homeDto);
    }
}
