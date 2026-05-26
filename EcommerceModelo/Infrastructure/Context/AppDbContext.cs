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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ── Tabelas ──────────────────────────────────────────────
            modelBuilder.Entity<Categoria>()      .ToTable("categorias");
            modelBuilder.Entity<Produto>()        .ToTable("produtos");
            modelBuilder.Entity<CategoriaProduto>().ToTable("categorias_produtos");
            modelBuilder.Entity<ProdutoImagem>()  .ToTable("Produto_imagens");
            modelBuilder.Entity<Compra>()         .ToTable("compras");
            modelBuilder.Entity<CompraItem>()     .ToTable("compra_itens");

            // ── Chave composta — CategoriaProduto (N:N) ──────────────
            modelBuilder.Entity<CategoriaProduto>()
                .HasKey(cp => new { cp.ProdutoId, cp.CategoriaId });
        }
    }
}
