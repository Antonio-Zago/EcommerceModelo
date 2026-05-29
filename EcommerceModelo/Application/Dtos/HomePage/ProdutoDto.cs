using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.HomePage
{
    public class ProdutoDto
    {
        public string Nome { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public string Descricao { get; set; } = string.Empty;

        public int QtdEstoque { get; set; }

        public string Tamanho { get; set; }

        public string ImagemPrincipalUrl { get; set; }
    }
}
