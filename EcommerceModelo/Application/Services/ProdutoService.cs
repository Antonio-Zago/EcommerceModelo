using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class ProdutoService : BaseService<Produto>, IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IProdutoImagemRepository _produtoImagemRepository;

    public ProdutoService(IProdutoRepository repository, IProdutoImagemRepository produtoImagemRepository) : base(repository)
    {
        _produtoRepository = repository;
        _produtoImagemRepository = produtoImagemRepository;
    }

    public Task<IEnumerable<Produto>> ObterTodosComImagensAsync()
        => _produtoRepository.ObterTodosComImagensAsync();

    public async Task<int> CadastrarComImagensAsync(Produto produto, List<(Stream stream, string nomeOriginal)> imagens, int imagemPrincipalIndex, string pastaFisica)
    {
        await _produtoRepository.AdicionarAsync(produto);

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

            var imagem = new ProdutoImagem
            {
                ProdutoId = produto.Id,
                ImagemUrl = $"/images/produtos/{nomeArquivo}",
                Principal = i == imagemPrincipalIndex
            };

            await _produtoImagemRepository.AdicionarAsync(imagem);
        }

        return produto.Id;
    }
}
