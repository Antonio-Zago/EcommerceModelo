using Application.Dtos.HomePage;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceModeloMvc.Controllers
{
    public class RoupasController : Controller
    {
        public IActionResult Novidades()
        {
            var produtos = new List<ProdutoDto>() {
                new ProdutoDto { Nome = "Blazer Oversized",   ImagemPrincipalUrl = "/images/modelo.jfif"  },
                new ProdutoDto { Nome = "Calça Wide Leg",     ImagemPrincipalUrl = "/images/modelo2.jpg"  },
                new ProdutoDto { Nome = "Top Cropped",        ImagemPrincipalUrl = "/images/modelo.jfif"  },
                new ProdutoDto { Nome = "Vestido Midi",       ImagemPrincipalUrl = "/images/modelo2.jpg"  },
                new ProdutoDto { Nome = "Camisa Linho",       ImagemPrincipalUrl = "/images/modelo.jfif"  },
                new ProdutoDto { Nome = "Jaqueta Jeans",      ImagemPrincipalUrl = "/images/modelo2.jpg"  },
                new ProdutoDto { Nome = "Short Alfaiataria",  ImagemPrincipalUrl = "/images/modelo.jfif"  },
            };

            return View(produtos);
        }

        public IActionResult ListarRoupasInfantis()
        {
            var produtos = new List<ProdutoDto>() {
                new ProdutoDto { Nome = "Camiseta Colorida",  ImagemPrincipalUrl = "/images/modelo.jfif"  },
                new ProdutoDto { Nome = "Calça Jogger",       ImagemPrincipalUrl = "/images/modelo2.jpg"  },
                new ProdutoDto { Nome = "Vestido Floral",     ImagemPrincipalUrl = "/images/modelo.jfif"  },
                new ProdutoDto { Nome = "Moletom Estampado",  ImagemPrincipalUrl = "/images/modelo2.jpg"  },
                new ProdutoDto { Nome = "Short Esportivo",    ImagemPrincipalUrl = "/images/modelo.jfif"  },
                new ProdutoDto { Nome = "Blusa de Frio",      ImagemPrincipalUrl = "/images/modelo2.jpg"  },
                new ProdutoDto { Nome = "Conjunto Listrado",  ImagemPrincipalUrl = "/images/modelo.jfif"  },
            };

            return View(produtos);
        }

        public IActionResult ListarRoupasMasculinas()
        {
            var produtos = new List<ProdutoDto>() {
                new ProdutoDto { Nome = "Camisa Social", ImagemPrincipalUrl = "/images/modelo.jfif" },
                new ProdutoDto { Nome = "Calça Slim", ImagemPrincipalUrl = "/images/modelo2.jpg" },
                new ProdutoDto { Nome = "Polo Clássica", ImagemPrincipalUrl = "/images/modelo.jfif" },
                new ProdutoDto { Nome = "Camiseta Básica", ImagemPrincipalUrl = "/images/modelo.jfif" },
                new ProdutoDto { Nome = "Bermuda Linen", ImagemPrincipalUrl = "/images/modelo2.jpg" },
                new ProdutoDto { Nome = "Jaqueta Leve", ImagemPrincipalUrl = "/images/modelo.jfif" },
                new ProdutoDto { Nome = "Short Casual", ImagemPrincipalUrl = "/images/modelo2.jpg" },
            };

            return View(produtos);
        }

        public IActionResult ListarRoupasFemininas()
        {
            var produtosMaisVendidosDtos = new List<ProdutoDto>() {
                new ProdutoDto {Nome = "Camisa1", ImagemPrincipalUrl = "/images/modelo.jfif" },
                new ProdutoDto {Nome = "Camisa2", ImagemPrincipalUrl = "/images/modelo2.jpg" },
                new ProdutoDto {Nome = "Camisa1", ImagemPrincipalUrl = "/images/modelo.jfif" },
                new ProdutoDto {Nome = "Camisa1", ImagemPrincipalUrl = "/images/modelo.jfif" },
                new ProdutoDto {Nome = "Camisa1", ImagemPrincipalUrl = "/images/modelo.jfif" },
                new ProdutoDto {Nome = "Camisa1", ImagemPrincipalUrl = "/images/modelo.jfif" },
                new ProdutoDto {Nome = "Camisa1", ImagemPrincipalUrl = "/images/modelo.jfif" },
            };

            return View(produtosMaisVendidosDtos);
        }
    }
}
