using System.Diagnostics;
using EcommerceModeloMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceModeloMvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Mock de categorias
            var categorias = new List<Categoria>
                {
                    new Categoria { Id = 1, Nome = "Camisetas" },
                    new Categoria { Id = 2, Nome = "Calças" },
                    new Categoria { Id = 3, Nome = "Vestidos" },
                    new Categoria { Id = 4, Nome = "Acessórios" }
                };

            // Passa as categorias para a View
            ViewBag.Categorias = categorias;

            return View();
        }
    }
}
