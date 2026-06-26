namespace Domain.Models;

public class CarrinhoItem
{
    public int ProdutoId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public string? ImagemUrl { get; set; }
    public int? TamanhoId { get; set; }
    public string? TamanhoNome { get; set; }
    public int Quantidade { get; set; }

    public decimal Subtotal => Preco * Quantidade;
}
