using Domain.Models;

namespace Application.Interfaces;

public interface ICarrinhoService
{
    Task<Carrinho> ObterCarrinhoAsync(int usuarioId);
    Task AdicionarItemAsync(int usuarioId, CarrinhoItem item);
    Task RemoverItemAsync(int usuarioId, int produtoId, int? tamanhoId);
    Task AtualizarQuantidadeAsync(int usuarioId, int produtoId, int? tamanhoId, int quantidade);
    Task LimparCarrinhoAsync(int usuarioId);
}
