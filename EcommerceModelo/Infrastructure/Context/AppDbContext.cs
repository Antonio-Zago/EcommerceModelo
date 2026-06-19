using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Compra> Compras { get; set; }
    public DbSet<CompraItem> CompraItens { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<ProdutoImagem> ProdutoImagens { get; set; }
    public DbSet<ProdutoEstoque> ProdutoEstoques { get; set; }
    public DbSet<TipoTamanho> TipoTamanhos { get; set; }
    public DbSet<OpcaoTamanho> OpcaoTamanhos { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Papel> Papeis { get; set; }
    public DbSet<PapelUsuario> PapeisUsuario { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>().ToTable("categorias");
        modelBuilder.Entity<Produto>().ToTable("produtos");
        modelBuilder.Entity<ProdutoImagem>().ToTable("produto_imagens");
        modelBuilder.Entity<ProdutoEstoque>().ToTable("produto_estoque");
        modelBuilder.Entity<Compra>().ToTable("compras");
        modelBuilder.Entity<CompraItem>().ToTable("compra_itens");
        modelBuilder.Entity<TipoTamanho>().ToTable("tipos_tamanhos");
        modelBuilder.Entity<OpcaoTamanho>().ToTable("opcao_tamanhos");
        modelBuilder.Entity<Usuario>().ToTable("usuarios");
        modelBuilder.Entity<Papel>().ToTable("papeis");
        modelBuilder.Entity<PapelUsuario>().ToTable("papel_usuarios");

        modelBuilder.Entity<CompraItem>()
            .HasKey(ci => new { ci.CompraId, ci.ProdutoId });

        modelBuilder.Entity<PapelUsuario>()
            .HasKey(pu => new { pu.UsuarioId, pu.PapelId });

        modelBuilder.Entity<PapelUsuario>()
            .HasOne(pu => pu.Usuario)
            .WithMany(u => u.PapeisUsuario)
            .HasForeignKey(pu => pu.UsuarioId);

        modelBuilder.Entity<PapelUsuario>()
            .HasOne(pu => pu.Papel)
            .WithMany(p => p.PapeisUsuario)
            .HasForeignKey(pu => pu.PapelId);

        modelBuilder.Entity<OpcaoTamanho>()
            .HasOne(o => o.Tipo)
            .WithMany(t => t.Opcoes)
            .HasForeignKey(o => o.TipoId);

        modelBuilder.Entity<ProdutoEstoque>()
            .HasOne(e => e.Tamanho)
            .WithMany(o => o.Estoques)
            .HasForeignKey(e => e.TamanhoId);

        modelBuilder.Entity<Produto>()
            .HasOne(p => p.Categoria)
            .WithMany(c => c.Produtos)
            .HasForeignKey(p => p.CategoriaId);
    }
}
