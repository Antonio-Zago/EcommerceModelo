using Application.Dtos.HomePage;

namespace Application.Interfaces;

public interface IHomeService
{
    Task<HomeDto> ObterHomeDtoAsync();
}
