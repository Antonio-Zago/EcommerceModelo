using Application.Dtos.HomePage;
using EcommerceModeloMvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EcommerceModeloMvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var homeDto = new HomeDto();

            var categoriasDtos = new List<CategoriaDto>()
             {
                    new CategoriaDto { Nome = "Camisetas", ImagemBase64 = "/images/camisa2.jpg" },
                    new CategoriaDto { Nome = "Calças", ImagemBase64 = "/images/calca2.jpg" },
                    new CategoriaDto { Nome = "Vestidos", ImagemBase64 = "/images/vestido2.jpg" },
             };

            var produtosMaisVendidosDtos = new List<ProdutoDto>() {
                new ProdutoDto {Nome = "Camisa1", FotoPrincipalBase64 = "/images/modelo.jfif" },
                new ProdutoDto {Nome = "Camisa2", FotoPrincipalBase64 = "/images/modelo2.jpg" }
            };

            homeDto.Produtos = produtosMaisVendidosDtos;
            homeDto.Categorias = categoriasDtos;

            return View(homeDto);
        }
    }
}
