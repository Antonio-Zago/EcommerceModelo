using Domain.Models;

namespace EcommerceModeloMvc.ViewModels;

public class GestaoUsuariosViewModel
{
    public IEnumerable<Usuario> Usuarios { get; set; } = [];
    public IEnumerable<Papel> Papeis { get; set; } = [];
}
