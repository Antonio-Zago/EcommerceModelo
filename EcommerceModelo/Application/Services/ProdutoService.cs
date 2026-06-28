using Application.Interfaces;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class ProdutoService : BaseService<Produto>, IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IProdutoImagemRepository _produtoImagemRepository;
    private readonly IProdutoEstoqueRepository _produtoEstoqueRepository;

    public ProdutoService(
        IProdutoRepository repository,
        IProdutoImagemRepository produtoImagemRepository,
        IProdutoEstoqueRepository produtoEstoqueRepository) : base(repository)
    {
        _produtoRepository = repository;
        _produtoImagemRepository = produtoImagemRepository;
        _produtoEstoqueRepository = produtoEstoqueRepository;
    }

    public Task<IEnumerable<Produto>> ObterTodosComImagensAsync()
        => _produtoRepository.ObterTodosComImagensAsync();

    public Task<IEnumerable<Produto>> ObterPorGeneroComImagensAsync(Genero genero)
        => _produtoRepository.ObterPorGeneroComImagensAsync(genero);

    public Task<IEnumerable<Produto>> ObterInfantisComImagensAsync()
        => _produtoRepository.ObterInfantisComImagensAsync();

    public async Task CadastrarComEstoqueAsync(
        Produto produto,
        List<(int tamanhoId, int quantidade)> estoques,
        List<(Stream stream, string nomeOriginal)> imagens,
        int imagemPrincipalIndex,
        string pastaFisica)
    {
        // 1. Persiste o produto
        await _produtoRepository.AdicionarAsync(produto);

        // 2. Salva os arquivos de imagem e vincula ao produto
        if (!Directory.Exists(pastaFisica))
            Directory.CreateDirectory(pastaFisica);

        for (int i = 0; i < imagens.Count; i++)
        {
            var (stream, nomeOriginal) = imagens[i];
            var extensao = Path.GetExtension(nomeOriginal).ToLowerInvariant();
            var nomeArquivo = $"{Guid.NewGuid()}{extensao}";
            var caminhoFisico = Path.Combine(pastaFisica, nomeArquivo);

            using var fs = new FileStream(caminhoFisico, FileMode.Create);
            await stream.CopyToAsync(fs);

            await _produtoImagemRepository.AdicionarAsync(new ProdutoImagem
            {
                ProdutoId = produto.Id,
                ImagemUrl = $"/images/produtos/{nomeArquivo}",
                Principal = i == imagemPrincipalIndex
            });
        }

        // 3. Registra um ProdutoEstoque por opcao de tamanho (FK)
        foreach (var (tamanhoId, quantidade) in estoques)
        {
            await _produtoEstoqueRepository.AdicionarAsync(new ProdutoEstoque
            {
                ProdutoId = produto.Id,
                TamanhoId = tamanhoId,
                Quantidade = quantidade
            });
        }
    }
}
