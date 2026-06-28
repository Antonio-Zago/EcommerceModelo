using System.ComponentModel.DataAnnotations;
using Domain.Enums;

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

    public List<TamanhoEstoqueViewModel> Tamanhos { get; set; } = new();

    [Required(ErrorMessage = "A categoria é obrigatória")]
    public int CategoriaId { get; set; }

    [Required(ErrorMessage = "O gênero é obrigatório.")]
    public Genero Genero { get; set; }

    public bool EhInfantil { get; set; }

    public List<IFormFile> Imagens { get; set; } = new();

    public int ImagemPrincipalIndex { get; set; } = 0;
}
