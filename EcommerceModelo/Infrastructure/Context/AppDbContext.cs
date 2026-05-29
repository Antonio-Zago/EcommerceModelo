using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<CategoriaProduto> CategoriasProdutos { get; set; }
    public DbSet<Compra> Compras { get; set; }
    public DbSet<CompraItem> CompraItens { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<ProdutoImagem> ProdutoImagens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>().ToTable("categorias");
        modelBuilder.Entity<Produto>().ToTable("produtos");
        modelBuilder.Entity<CategoriaProduto>().ToTable("categorias_produtos");
        modelBuilder.Entity<ProdutoImagem>().ToTable("produto_imagens");
        modelBuilder.Entity<Compra>().ToTable("compras");
        modelBuilder.Entity<CompraItem>().ToTable("compra_itens");

        modelBuilder.Entity<CategoriaProduto>()
            .HasKey(cp => new { cp.ProdutoId, cp.CategoriaId });

        modelBuilder.Entity<CompraItem>()
            .HasKey(ci => new { ci.CompraId, ci.ProdutoId });
    }
}
