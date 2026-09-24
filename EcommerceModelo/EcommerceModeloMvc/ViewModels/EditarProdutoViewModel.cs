using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace EcommerceModeloMvc.ViewModels;

public class EditarProdutoViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [MaxLength(200, ErrorMessage = "O nome pode ter no máximo 200 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O preço é obrigatório.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
    public decimal Preco { get; set; }

    [Required(ErrorMessage = "A descrição é obrigatória.")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "A categoria é obrigatória")]
    public int CategoriaId { get; set; }

    [Required(ErrorMessage = "O gênero é obrigatório.")]
    public Genero Genero { get; set; }

    public bool EhInfantil { get; set; }

    public StatusProduto Status { get; set; } = StatusProduto.Ativo;

    public bool JaVendido { get; set; }

    public List<EstoqueEdicaoViewModel> Estoques { get; set; } = new();

    public List<ImagemEdicaoViewModel> Imagens { get; set; } = new();

    public int? ImagemPrincipalId { get; set; }
}

public class EstoqueEdicaoViewModel
{
    public int Id { get; set; }

    public string Tamanho { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe a quantidade.")]
    [Range(0, int.MaxValue, ErrorMessage = "A quantidade não pode ser negativa.")]
    public int Quantidade { get; set; }
}

public class ImagemEdicaoViewModel
{
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;
}
