using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Avaliacao;

public class AvaliarProdutoDto
{
    [Required]
    public int ProdutoId { get; set; }

    [Required(ErrorMessage = "Selecione uma nota")]
    [Range(1, 5, ErrorMessage = "A nota deve ser entre 1 e 5")]
    public int Nota { get; set; }

    [StringLength(2000, ErrorMessage = "A descrição deve ter no máximo 2000 caracteres")]
    public string? Descricao { get; set; }
}
