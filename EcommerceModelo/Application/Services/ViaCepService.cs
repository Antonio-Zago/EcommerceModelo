using Application.Dtos.Checkout;
using Application.Interfaces;
using System.Net.Http.Json;

namespace Application.Services;

public class ViaCepService : IViaCepService
{
    private readonly HttpClient _httpClient;

    public ViaCepService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<EnderecoViaCepDto?> BuscarPorCepAsync(string cep)
    {
        var cepLimpo = new string(cep.Where(char.IsDigit).ToArray());

        if (cepLimpo.Length != 8)
            return null;

        try
        {
            var resultado = await _httpClient.GetFromJsonAsync<EnderecoViaCepDto>(
                $"ws/{cepLimpo}/json/");

            return resultado?.Erro == true ? null : resultado;
        }
        catch
        {
            return null;
        }
    }
}
