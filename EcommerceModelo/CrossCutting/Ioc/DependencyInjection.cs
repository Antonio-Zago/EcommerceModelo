using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.BackgroundJobs;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.Ioc;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfigurationManager configuration)
    {
        var postgresConnection = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(postgresConnection));

        // Repositories
        services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<ICompraRepository, CompraRepository>();
        services.AddScoped<ICompraItemRepository, CompraItemRepository>();
        services.AddScoped<IProdutoImagemRepository, ProdutoImagemRepository>();
        services.AddScoped<IProdutoEstoqueRepository, ProdutoEstoqueRepository>();
        services.AddScoped<IOpcaoTamanhoRepository, OpcaoTamanhoRepository>();
        services.AddScoped<ITipoTamanhoRepository, TipoTamanhoRepository>();

        // Services
        services.AddScoped<IHomeService, HomeService>();
        services.AddScoped<ICategoriaService, CategoriaService>();
        services.AddScoped<IProdutoService, ProdutoService>();
        services.AddScoped<ICompraService, CompraService>();
        services.AddScoped<ICompraItemService, CompraItemService>();
        services.AddScoped<IProdutoImagemService, ProdutoImagemService>();
        services.AddScoped<IProdutoEstoqueService, ProdutoEstoqueService>();
        services.AddScoped<IOpcaoTamanhoService, OpcaoTamanhoService>();
        services.AddScoped<ITipoTamanhoService, TipoTamanhoService>();
        services.AddScoped<IImportacaoProdutosService, ImportacaoProdutosService>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IPapelRepository, PapelRepository>();
        services.AddScoped<IPapelUsuarioRepository, PapelUsuarioRepository>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IPapelService, PapelService>();
        services.AddScoped<ICarrinhoService, CarrinhoService>();
        services.AddScoped<ICheckoutService, CheckoutService>();

        services.AddHttpClient<IViaCepService, ViaCepService>(client =>
        {
            client.BaseAddress = new Uri("https://viacep.com.br/");
            client.Timeout = TimeSpan.FromSeconds(5);
        });

        // Background jobs — Singleton: estado compartilhado entre requests e worker
        services.AddSingleton<IJobStore, JobStore>();
        services.AddSingleton<IImportacaoQueue, ImportacaoQueue>();

        return services;
    }
}
