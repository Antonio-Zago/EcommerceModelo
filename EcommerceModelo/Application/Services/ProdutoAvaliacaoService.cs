using Application.Dtos.Avaliacao;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class ProdutoAvaliacaoService : BaseService<ProdutoAvaliacao>, IProdutoAvaliacaoService
{
    private readonly IProdutoAvaliacaoRepository _avaliacaoRepository;
    private readonly ICompraRepository _compraRepository;

    public ProdutoAvaliacaoService(IProdutoAvaliacaoRepository repository, ICompraRepository compraRepository) : base(repository)
    {
        _avaliacaoRepository = repository;
        _compraRepository = compraRepository;
    }

    public Task<IEnumerable<ProdutoAvaliacao>> ObterPorUsuarioAsync(int usuarioId)
        => _avaliacaoRepository.ObterPorUsuarioAsync(usuarioId);

    // Cria a avaliação ou atualiza a existente — cada usuário tem uma avaliação por produto
    public async Task AvaliarAsync(int usuarioId, AvaliarProdutoDto avaliacao)
    {
        if (!await _compraRepository.UsuarioComprouProdutoAsync(usuarioId, avaliacao.ProdutoId))
            throw new InvalidOperationException("Só é possível avaliar produtos que você comprou.");

        var descricao = string.IsNullOrWhiteSpace(avaliacao.Descricao) ? null : avaliacao.Descricao.Trim();
        var existente = await _avaliacaoRepository.ObterPorUsuarioEProdutoAsync(usuarioId, avaliacao.ProdutoId);

        if (existente is null)
        {
            await _avaliacaoRepository.AdicionarAsync(new ProdutoAvaliacao
            {
                ProdutoId = avaliacao.ProdutoId,
                UsuarioId = usuarioId,
                Nota = avaliacao.Nota,
                Descricao = descricao
            });
            return;
        }

        existente.Nota = avaliacao.Nota;
        existente.Descricao = descricao;
        await _avaliacaoRepository.AtualizarAsync(existente);
    }
}
