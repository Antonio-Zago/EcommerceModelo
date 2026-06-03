using System.ComponentModel.DataAnnotations;

namespace EcommerceModeloMvc.ViewModels;

public class TamanhoEstoqueViewModel
{
    [Required(ErrorMessage = "Selecione um tamanho.")]
    public string Tamanho { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe a quantidade.")]
    [Range(0, int.MaxValue, ErrorMessage = "A quantidade não pode ser negativa.")]
    public int QtdEstoque { get; set; }
}
