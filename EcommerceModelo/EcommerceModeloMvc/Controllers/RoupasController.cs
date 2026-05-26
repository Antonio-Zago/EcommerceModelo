using Application.Dtos.HomePage;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceModeloMvc.Controllers
{
    public class RoupasController : Controller
    {
        public IActionResult Novidades()
        {
            var produtos = new List<ProdutoDto>() {
                new ProdutoDto { Nome = "Blazer Oversized",   FotoPrincipalBase64 = "/images/modelo.jfif"  },
                new ProdutoDto { Nome = "Calça Wide Leg",     FotoPrincipalBase64 = "/images/modelo2.jpg"  },
                new ProdutoDto { Nome = "Top Cropped",        FotoPrincipalBase64 = "/images/modelo.jfif"  },
                new ProdutoDto { Nome = "Vestido Midi",       FotoPrincipalBase64 = "/images/modelo2.jpg"  },
                new ProdutoDto { Nome = "Camisa Linho",       FotoPrincipalBase64 = "/images/modelo.jfif"  },
                new ProdutoDto { Nome = "Jaqueta Jeans",      FotoPrincipalBase64 = "/images/modelo2.jpg"  },
                new ProdutoDto { Nome = "Short Alfaiataria",  FotoPrincipalBase64 = "/images/modelo.jfif"  },
            };

            return View(produtos);
        }

        public IActionResult ListarRoupasInfantis()
        {
            var produtos = new List<ProdutoDto>() {
                new ProdutoDto { Nome = "Camiseta Colorida",  FotoPrincipalBase64 = "/images/modelo.jfif"  },
                new ProdutoDto { Nome = "Calça Jogger",       FotoPrincipalBase64 = "/images/modelo2.jpg"  },
                new ProdutoDto { Nome = "Vestido Floral",     FotoPrincipalBase64 = "/images/modelo.jfif"  },
                new ProdutoDto { Nome = "Moletom Estampado",  FotoPrincipalBase64 = "/images/modelo2.jpg"  },
                new ProdutoDto { Nome = "Short Esportivo",    FotoPrincipalBase64 = "/images/modelo.jfif"  },
                new ProdutoDto { Nome = "Blusa de Frio",      FotoPrincipalBase64 = "/images/modelo2.jpg"  },
                new ProdutoDto { Nome = "Conjunto Listrado",  FotoPrincipalBase64 = "/images/modelo.jfif"  },
            };

            return View(produtos);
        }

        public IActionResult ListarRoupasMasculinas()
        {
            var produtos = new List<ProdutoDto>() {
                new ProdutoDto { Nome = "Camisa Social", FotoPrincipalBase64 = "/images/modelo.jfif" },
                new ProdutoDto { Nome = "Calça Slim", FotoPrincipalBase64 = "/images/modelo2.jpg" },
                new ProdutoDto { Nome = "Polo Clássica", FotoPrincipalBase64 = "/images/modelo.jfif" },
                new ProdutoDto { Nome = "Camiseta Básica", FotoPrincipalBase64 = "/images/modelo.jfif" },
                new ProdutoDto { Nome = "Bermuda Linen", FotoPrincipalBase64 = "/images/modelo2.jpg" },
                new ProdutoDto { Nome = "Jaqueta Leve", FotoPrincipalBase64 = "/images/modelo.jfif" },
                new ProdutoDto { Nome = "Short Casual", FotoPrincipalBase64 = "/images/modelo2.jpg" },
            };

            return View(produtos);
        }

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
