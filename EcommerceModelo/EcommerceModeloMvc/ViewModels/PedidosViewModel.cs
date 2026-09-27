using Domain.Models;

namespace EcommerceModeloMvc.ViewModels;

public class PedidosViewModel
{
    public IEnumerable<Compra> Pedidos { get; set; } = [];

    // Avaliações do usuário indexadas pelo id do produto
    public IReadOnlyDictionary<int, ProdutoAvaliacao> Avaliacoes { get; set; } = new Dictionary<int, ProdutoAvaliacao>();
}
