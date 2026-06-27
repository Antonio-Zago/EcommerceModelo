using Application.Dtos.Checkout;
using Domain.Models;

namespace EcommerceModeloMvc.ViewModels;

public class CheckoutViewModel
{
    public Carrinho Carrinho { get; set; } = null!;
    public ConfirmarPedidoDto Pedido { get; set; } = new();
}
