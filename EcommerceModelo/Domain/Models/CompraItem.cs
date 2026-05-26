using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceModeloMvc.Models
{
    public class CompraItem
    {
        [Column("produto")]
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; } = null!;

        [Column("compra")]
        public int CompraId { get; set; }
        public Compra Compra { get; set; } = null!;
    }
}
