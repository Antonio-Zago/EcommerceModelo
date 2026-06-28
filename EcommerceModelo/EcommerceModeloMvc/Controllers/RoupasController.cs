using Application.Dtos.HomePage;
using Application.Interfaces;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceModeloMvc.Controllers
{
    public class RoupasController : Controller
    {
        private readonly IProdutoService _produtoService;

        public RoupasController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        public async Task<IActionResult> Novidades()
        {
            var produtos = await _produtoService.ObterTodosComImagensAsync();
            return View(MapearDtos(produtos));
        }

        public async Task<IActionResult> ListarRoupasInfantis()
        {
            var produtos = await _produtoService.ObterInfantisComImagensAsync();
            return View(MapearDtos(produtos));
        }

        public async Task<IActionResult> ListarRoupasMasculinas()
        {
            var produtos = await _produtoService.ObterPorGeneroComImagensAsync(Genero.Masculino);
            return View(MapearDtos(produtos));
        }

        public async Task<IActionResult> ListarRoupasFemininas()
        {
            var produtos = await _produtoService.ObterPorGeneroComImagensAsync(Genero.Feminino);
            return View(MapearDtos(produtos));
        }

        private static List<ProdutoDto> MapearDtos(IEnumerable<Produto> produtos)
            => produtos.Select(p => new ProdutoDto
            {
                Nome = p.Nome,
                Preco = p.Preco,
                Descricao = p.Descricao,
                ImagemPrincipalUrl = p.Imagens.FirstOrDefault(i => i.Principal)?.ImagemUrl
                    ?? p.Imagens.FirstOrDefault()?.ImagemUrl
                    ?? string.Empty
            }).ToList();
    }
}
