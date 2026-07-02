using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceModeloMvc.Controllers;

[Authorize(Roles = "Admin")]
public class AdministracaoController : Controller
{
    public IActionResult Index() => View();
}
