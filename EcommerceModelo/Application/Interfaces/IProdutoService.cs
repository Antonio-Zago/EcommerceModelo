using Domain.Models;

namespace Application.Interfaces;

public interface IProdutoService : IBaseService<Produto>
{
    Task<IEnumerable<Produto>> ObterTodosComImagensAsync();

    /// <param name="imagens">Lista de (stream do arquivo, nome original) para salvar.</param>
    Task<int> CadastrarComImagensAsync(Produto produto, List<(Stream stream, string nomeOriginal)> imagens, int imagemPrincipalIndex, string pastaFisica);
}
