using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Checkout;

public class ConfirmarPedidoDto
{
    [Required(ErrorMessage = "CEP obrigatório")]
    public string Cep { get; set; } = string.Empty;

    [Required(ErrorMessage = "Rua obrigatória")]
    public string Rua { get; set; } = string.Empty;

    [Required(ErrorMessage = "Número obrigatório")]
    public string Numero { get; set; } = string.Empty;

    public string? Complemento { get; set; }

    [Required(ErrorMessage = "Bairro obrigatório")]
    public string Bairro { get; set; } = string.Empty;

    [Required(ErrorMessage = "Cidade obrigatória")]
    public string Cidade { get; set; } = string.Empty;

    [Required(ErrorMessage = "UF obrigatória")]
    public string Uf { get; set; } = string.Empty;

    [Required(ErrorMessage = "Selecione a forma de pagamento")]
    public string FormaPagamento { get; set; } = string.Empty;
}
