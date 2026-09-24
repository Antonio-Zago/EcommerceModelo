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
    private readonly ICompraItemRepository _compraItemRepository;

    public ProdutoService(
        IProdutoRepository repository,
        IProdutoImagemRepository produtoImagemRepository,
        IProdutoEstoqueRepository produtoEstoqueRepository,
        ICompraItemRepository compraItemRepository) : base(repository)
    {
        _produtoRepository = repository;
        _produtoImagemRepository = produtoImagemRepository;
        _produtoEstoqueRepository = produtoEstoqueRepository;
        _compraItemRepository = compraItemRepository;
    }

    public Task<bool> ProdutoJaVendidoAsync(int produtoId)
        => _compraItemRepository.ProdutoJaVendidoAsync(produtoId);

    public async Task<bool> ArquivarAsync(int produtoId)
    {
        var produto = await _produtoRepository.ObterPorIdAsync(produtoId);
        if (produto is null)
            return false;

        produto.Status = StatusProduto.Arquivado;
        await _produtoRepository.AtualizarAsync(produto);
        return true;
    }

    public async Task<Produto?> DuplicarAsync(int produtoId, string webRootPath)
    {
        var original = await _produtoRepository.ObterPorIdComDetalhesAsync(produtoId);
        if (original is null)
            return null;

        var copia = new Produto
        {
            Nome = original.Nome,
            Preco = original.Preco,
            Descricao = original.Descricao,
            CategoriaId = original.CategoriaId,
            Genero = original.Genero,
            EhInfantil = original.EhInfantil,
            Status = StatusProduto.Ativo,
            DataCadastro = DateTime.UtcNow
        };
        await _produtoRepository.AdicionarAsync(copia);

        var pastaImagens = Path.Combine(webRootPath, "images", "produtos");
        var raizWeb = Path.GetFullPath(webRootPath);
        Directory.CreateDirectory(pastaImagens);

        foreach (var imagem in original.Imagens)
        {
            var urlNova = imagem.ImagemUrl;
            var caminhoOrigem = Path.GetFullPath(Path.Combine(webRootPath, imagem.ImagemUrl.TrimStart('/', '\\')));

            if (caminhoOrigem.StartsWith(raizWeb, StringComparison.OrdinalIgnoreCase) && File.Exists(caminhoOrigem))
            {
                var nomeArquivo = $"{Guid.NewGuid()}{Path.GetExtension(caminhoOrigem).ToLowerInvariant()}";
                File.Copy(caminhoOrigem, Path.Combine(pastaImagens, nomeArquivo));
                urlNova = $"/images/produtos/{nomeArquivo}";
            }

            await _produtoImagemRepository.AdicionarAsync(new ProdutoImagem
            {
                ProdutoId = copia.Id,
                ImagemUrl = urlNova,
                Principal = imagem.Principal
            });
        }

        foreach (var estoque in original.Estoques)
        {
            await _produtoEstoqueRepository.AdicionarAsync(new ProdutoEstoque
            {
                ProdutoId = copia.Id,
                TamanhoId = estoque.TamanhoId,
                Quantidade = estoque.Quantidade
            });
        }

        return copia;
    }

    public Task<IEnumerable<Produto>> ObterTodosComImagensAsync()
        => _produtoRepository.ObterTodosComImagensAsync();

    public Task<IEnumerable<Produto>> ObterPorGeneroComImagensAsync(Genero genero)
        => _produtoRepository.ObterPorGeneroComImagensAsync(genero);

    public Task<IEnumerable<Produto>> ObterInfantisComImagensAsync()
        => _produtoRepository.ObterInfantisComImagensAsync();

    public Task<Produto?> ObterPorIdComDetalhesAsync(int id)
        => _produtoRepository.ObterPorIdComDetalhesAsync(id);

    public Task<IEnumerable<Produto>> ObterTodosComDetalhesAsync()
        => _produtoRepository.ObterTodosComDetalhesAsync();

    public async Task<bool> AtualizarComEstoqueAsync(
        Produto dados,
        Dictionary<int, int> quantidadesPorEstoqueId,
        int? imagemPrincipalId)
    {
        var produto = await _produtoRepository.ObterPorIdComDetalhesAsync(dados.Id);
        if (produto is null)
            return false;

        if (await _compraItemRepository.ProdutoJaVendidoAsync(produto.Id))
            throw new InvalidOperationException("Produtos que já foram vendidos não podem ser editados, apenas arquivados.");

        produto.Status = dados.Status;
        produto.Nome = dados.Nome;
        produto.Preco = dados.Preco;
        produto.Descricao = dados.Descricao;
        produto.CategoriaId = dados.CategoriaId;
        produto.Genero = dados.Genero;
        produto.EhInfantil = dados.EhInfantil;

        foreach (var estoque in produto.Estoques)
        {
            if (quantidadesPorEstoqueId.TryGetValue(estoque.Id, out var quantidade))
                estoque.Quantidade = quantidade;
        }

        if (imagemPrincipalId.HasValue && produto.Imagens.Any(i => i.Id == imagemPrincipalId.Value))
        {
            foreach (var imagem in produto.Imagens)
                imagem.Principal = imagem.Id == imagemPrincipalId.Value;
        }

        await _produtoRepository.AtualizarAsync(produto);
        return true;
    }

    public async Task CadastrarComEstoqueAsync(
        Produto produto,
        List<(int tamanhoId, int quantidade)> estoques,
        List<(Stream stream, string nomeOriginal)> imagens,
        int imagemPrincipalIndex,
        string pastaFisica)
    {
        // 1. Persiste o produto
        produto.DataCadastro = DateTime.UtcNow;
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
