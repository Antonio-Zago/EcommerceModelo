using System.ComponentModel.DataAnnotations;

namespace EcommerceModeloMvc.ViewModels;

public class CadastroProdutoViewModel
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [MaxLength(200, ErrorMessage = "O nome pode ter no máximo 200 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O preço é obrigatório.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
    public decimal Preco { get; set; }

    [Required(ErrorMessage = "A descrição é obrigatória.")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "A quantidade em estoque é obrigatória.")]
    [Range(0, int.MaxValue, ErrorMessage = "O estoque não pode ser negativo.")]
    public int QtdEstoque { get; set; }

    [Required(ErrorMessage = "O tamanho é obrigatório.")]
    public string Tamanho { get; set; } = string.Empty;

    public List<int> CategoriaIds { get; set; } = new();

    public List<IFormFile> Imagens { get; set; } = new();

    public int ImagemPrincipalIndex { get; set; } = 0;
}
