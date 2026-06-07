using Domain.Models;

namespace Application.Interfaces;

public interface IProdutoService : IBaseService<Produto>
{
    Task<IEnumerable<Produto>> ObterTodosComImagensAsync();

    /// <summary>
    /// Cadastra um Produto com suas imagens e registra os estoques por tamanho (FK) na tabela produto_estoque.
    /// </summary>
    Task CadastrarComEstoqueAsync(Produto produto, List<(int tamanhoId, int quantidade)> estoques, List<(Stream stream, string nomeOriginal)> imagens, int imagemPrincipalIndex, string pastaFisica);
}
