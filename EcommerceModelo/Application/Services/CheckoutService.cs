using Application.Dtos.Checkout;
using Application.Interfaces;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class CheckoutService : ICheckoutService
{
    private readonly ICompraRepository _compraRepository;
    private readonly IProdutoEstoqueRepository _estoqueRepository;

    public CheckoutService(ICompraRepository compraRepository, IProdutoEstoqueRepository estoqueRepository)
    {
        _compraRepository = compraRepository;
        _estoqueRepository = estoqueRepository;
    }

    public async Task<IReadOnlyList<string>> VerificarEstoqueAsync(Carrinho carrinho)
    {
        var estoques = (await _estoqueRepository
            .ObterPorProdutosAsync(carrinho.Itens.Select(i => i.ProdutoId)))
            .ToList();

        var erros = new List<string>();

        var solicitados = carrinho.Itens
            .GroupBy(i => (i.ProdutoId, i.TamanhoId))
            .Select(g => (g.Key.ProdutoId, g.Key.TamanhoId, Quantidade: g.Sum(i => i.Quantidade), Item: g.First()));

        foreach (var (produtoId, tamanhoId, quantidade, item) in solicitados)
        {
            var estoque = estoques.FirstOrDefault(e => e.ProdutoId == produtoId && e.TamanhoId == tamanhoId);
            var descricao = string.IsNullOrWhiteSpace(item.TamanhoNome)
                ? item.Nome
                : $"{item.Nome} (tamanho {item.TamanhoNome})";

            if (estoque is null || estoque.Produto.Status == StatusProduto.Arquivado || estoque.Quantidade <= 0)
                erros.Add($"\"{descricao}\" está indisponível no momento. Remova-o do carrinho para continuar.");
            else if (estoque.Quantidade < quantidade)
                erros.Add($"\"{descricao}\": restam apenas {estoque.Quantidade} unidade(s) em estoque, mas o carrinho tem {quantidade}.");
        }

        return erros;
    }

    public async Task<Compra> ConfirmarPedidoAsync(int usuarioId, ConfirmarPedidoDto dto, Carrinho carrinho)
    {
        var compra = new Compra
        {
            UsuarioId = usuarioId,
            Status = "confirmado",
            FormaPagamento = dto.FormaPagamento,
            Total = carrinho.Total,
            CriadoEm = DateTime.UtcNow,
            EntregaPrevista = CalcularEntregaPrevista(),
            Endereco = new Endereco
            {
                Cep = dto.Cep,
                Rua = dto.Rua,
                Numero = dto.Numero,
                Complemento = dto.Complemento,
                Bairro = dto.Bairro,
                Cidade = dto.Cidade,
                Uf = dto.Uf
            },
            Itens = carrinho.Itens.Select(i => new CompraItem
            {
                ProdutoId = i.ProdutoId,
                NomeProduto = i.Nome,
                PrecoUnitario = i.Preco,
                Quantidade = i.Quantidade,
                TamanhoId = i.TamanhoId
            }).ToList()
        };

        await _compraRepository.AdicionarComBaixaDeEstoqueAsync(compra);
        return compra;
    }

    // 7 dias úteis a partir de hoje
    private static DateOnly CalcularEntregaPrevista()
    {
        var data = DateOnly.FromDateTime(DateTime.Today);
        int diasUteis = 0;

        while (diasUteis < 7)
        {
            data = data.AddDays(1);
            if (data.DayOfWeek is not DayOfWeek.Saturday and not DayOfWeek.Sunday)
                diasUteis++;
        }

        return data;
    }
}
