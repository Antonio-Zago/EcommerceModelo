using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.HomePage
{
    public class HomeDto
    {
        public List<CategoriaDto> Categorias { get; set; }

        public List<ProdutoDto> Produtos { get; set; }
    }
}
