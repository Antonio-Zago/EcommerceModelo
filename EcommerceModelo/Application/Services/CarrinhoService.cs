using Application.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Application.Services;

public class CarrinhoService : ICarrinhoService
{
    private readonly IDistributedCache _cache;

    private static readonly DistributedCacheEntryOptions _opcaoCache = new()
    {
        SlidingExpiration = TimeSpan.FromDays(7)
    };

    public CarrinhoService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<Carrinho> ObterCarrinhoAsync(int usuarioId)
    {
        var json = await _cache.GetStringAsync(ChaveCarrinho(usuarioId));

        if (string.IsNullOrEmpty(json))
            return new Carrinho { UsuarioId = usuarioId };

        return JsonSerializer.Deserialize<Carrinho>(json)!;
    }

    public async Task AdicionarItemAsync(int usuarioId, CarrinhoItem item)
    {
        var carrinho = await ObterCarrinhoAsync(usuarioId);

        var existente = carrinho.Itens.FirstOrDefault(i =>
            i.ProdutoId == item.ProdutoId && i.TamanhoId == item.TamanhoId);

        if (existente is not null)
            existente.Quantidade += item.Quantidade;
        else
            carrinho.Itens.Add(item);

        carrinho.AtualizadoEm = DateTime.UtcNow;
        await SalvarAsync(usuarioId, carrinho);
    }

    public async Task RemoverItemAsync(int usuarioId, int produtoId, int? tamanhoId)
    {
        var carrinho = await ObterCarrinhoAsync(usuarioId);

        carrinho.Itens.RemoveAll(i => i.ProdutoId == produtoId && i.TamanhoId == tamanhoId);
        carrinho.AtualizadoEm = DateTime.UtcNow;

        await SalvarAsync(usuarioId, carrinho);
    }

    public async Task AtualizarQuantidadeAsync(int usuarioId, int produtoId, int? tamanhoId, int quantidade)
    {
        if (quantidade <= 0)
        {
            await RemoverItemAsync(usuarioId, produtoId, tamanhoId);
            return;
        }

        var carrinho = await ObterCarrinhoAsync(usuarioId);

        var item = carrinho.Itens.FirstOrDefault(i =>
            i.ProdutoId == produtoId && i.TamanhoId == tamanhoId);

        if (item is not null)
        {
            item.Quantidade = quantidade;
            carrinho.AtualizadoEm = DateTime.UtcNow;
            await SalvarAsync(usuarioId, carrinho);
        }
    }

    public async Task LimparCarrinhoAsync(int usuarioId)
    {
        await _cache.RemoveAsync(ChaveCarrinho(usuarioId));
    }

    private async Task SalvarAsync(int usuarioId, Carrinho carrinho)
    {
        var json = JsonSerializer.Serialize(carrinho);
        await _cache.SetStringAsync(ChaveCarrinho(usuarioId), json, _opcaoCache);
    }

    private static string ChaveCarrinho(int usuarioId) => $"carrinho:{usuarioId}";
}
