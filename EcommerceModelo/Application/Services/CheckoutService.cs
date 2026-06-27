using Application.Dtos.Checkout;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class CheckoutService : ICheckoutService
{
    private readonly ICompraRepository _compraRepository;

    public CheckoutService(ICompraRepository compraRepository)
    {
        _compraRepository = compraRepository;
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

        await _compraRepository.AdicionarAsync(compra);
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
