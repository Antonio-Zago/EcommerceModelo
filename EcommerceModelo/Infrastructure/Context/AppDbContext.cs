using EcommerceModeloMvc.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<CategoriaProduto> CategoriasProdutos { get; set; }

        public DbSet<Compra> Compras { get; set; }

        public DbSet<CompraItem> CompraItens { get; set; }

        public DbSet<Produto> Produtos { get; set; }

        public DbSet<ProdutoImagem> ProdutoImagens { get; set; }
    }
}
