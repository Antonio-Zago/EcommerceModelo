using System.ComponentModel.DataAnnotations;

namespace EcommerceModeloMvc.ViewModels;

public class ImportarProdutosViewModel
{
    [Required(ErrorMessage = "Selecione um arquivo Excel.")]
    public IFormFile? Arquivo { get; set; }
}
