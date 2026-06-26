namespace Domain.Models;

public class Carrinho
{
    public int UsuarioId { get; set; }
    public List<CarrinhoItem> Itens { get; set; } = [];
    public DateTime AtualizadoEm { get; set; } = DateTime.UtcNow;

    public decimal Total => Itens.Sum(i => i.Subtotal);
    public int QuantidadeTotal => Itens.Sum(i => i.Quantidade);
}
