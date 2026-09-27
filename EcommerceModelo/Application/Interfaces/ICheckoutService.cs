using Application.Dtos.Checkout;
using Domain.Models;

namespace Application.Interfaces;

public interface ICheckoutService
{
    Task<IReadOnlyList<string>> VerificarEstoqueAsync(Carrinho carrinho);
    Task<Compra> ConfirmarPedidoAsync(int usuarioId, ConfirmarPedidoDto dto, Carrinho carrinho);
}
