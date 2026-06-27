using Application.Dtos.Checkout;
using Domain.Models;

namespace Application.Interfaces;

public interface ICheckoutService
{
    Task<Compra> ConfirmarPedidoAsync(int usuarioId, ConfirmarPedidoDto dto, Carrinho carrinho);
}
