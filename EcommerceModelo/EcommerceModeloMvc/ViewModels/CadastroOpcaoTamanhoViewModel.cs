using System.ComponentModel.DataAnnotations;

namespace EcommerceModeloMvc.ViewModels;

public class CadastroOpcaoTamanhoViewModel
{
    [Required(ErrorMessage = "A descrição é obrigatória.")]
    [MaxLength(100, ErrorMessage = "A descrição pode ter no máximo 100 caracteres.")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "Selecione um tipo.")]
    public int TipoId { get; set; }
}
