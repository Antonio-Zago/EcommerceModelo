using System.ComponentModel.DataAnnotations;

namespace EcommerceModeloMvc.ViewModels;

public class CadastroTipoTamanhoViewModel
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O nome pode ter no máximo 100 caracteres.")]
    public string Nome { get; set; } = string.Empty;
}
