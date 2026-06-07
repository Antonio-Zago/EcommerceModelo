using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
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

        return services;
    }
}
