using Application.Dtos.HomePage;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceModeloMvc.Controllers
{
    public class RoupasController : Controller
    {
        public IActionResult ListarRoupasFemininas()
        {
            var produtosMaisVendidosDtos = new List<ProdutoDto>() {
                new ProdutoDto {Nome = "Camisa1", FotoPrincipalBase64 = "/images/modelo.jfif" },
                new ProdutoDto {Nome = "Camisa2", FotoPrincipalBase64 = "/images/modelo2.jpg" },
                new ProdutoDto {Nome = "Camisa1", FotoPrincipalBase64 = "/images/modelo.jfif" },
                new ProdutoDto {Nome = "Camisa1", FotoPrincipalBase64 = "/images/modelo.jfif" },
                new ProdutoDto {Nome = "Camisa1", FotoPrincipalBase64 = "/images/modelo.jfif" },
                new ProdutoDto {Nome = "Camisa1", FotoPrincipalBase64 = "/images/modelo.jfif" },
                new ProdutoDto {Nome = "Camisa1", FotoPrincipalBase64 = "/images/modelo.jfif" },
            };

            return View(produtosMaisVendidosDtos);
        }
    }
}
