using Domain.Enums;
using Domain.Models;

namespace Application.Interfaces;

public interface IProdutoService : IBaseService<Produto>
{
    Task<IEnumerable<Produto>> ObterTodosComImagensAsync();
    Task<IEnumerable<Produto>> ObterPorGeneroComImagensAsync(Genero genero);
    Task<IEnumerable<Produto>> ObterInfantisComImagensAsync();
    Task<Produto?> ObterPorIdComDetalhesAsync(int id);

    Task CadastrarComEstoqueAsync(Produto produto, List<(int tamanhoId, int quantidade)> estoques, List<(Stream stream, string nomeOriginal)> imagens, int imagemPrincipalIndex, string pastaFisica);
}
