using Application.Dtos.Checkout;

namespace Application.Interfaces;

public interface IViaCepService
{
    Task<EnderecoViaCepDto?> BuscarPorCepAsync(string cep);
}
