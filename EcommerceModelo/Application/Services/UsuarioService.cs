using Application.Dtos.Auth;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPapelUsuarioRepository _papelUsuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository, IPapelUsuarioRepository papelUsuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
        _papelUsuarioRepository = papelUsuarioRepository;
    }

    public async Task<Usuario> CadastrarAsync(CadastroUsuarioDto dto)
    {
        if (await _usuarioRepository.EmailExisteAsync(dto.Email))
            throw new InvalidOperationException("Já existe uma conta com este e-mail.");

        var usuario = new Usuario
        {
            Nome = dto.Nome,
            Email = dto.Email.ToLowerInvariant(),
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha)
        };

        await _usuarioRepository.AdicionarAsync(usuario);
        return usuario;
    }

    public async Task<Usuario?> AutenticarAsync(LoginDto dto)
    {
        var usuario = await _usuarioRepository.ObterPorEmailAsync(dto.Email.ToLowerInvariant());

        if (usuario is null || !BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.SenhaHash))
            return null;

        return usuario;
    }

    public Task<IEnumerable<Usuario>> ObterTodosComPapeisAsync()
        => _usuarioRepository.ObterTodosComPapeisAsync();

    public async Task AtribuirPapelAsync(int usuarioId, int papelId)
    {
        if (await _papelUsuarioRepository.ExisteAsync(usuarioId, papelId))
            return;

        await _papelUsuarioRepository.AdicionarAsync(new PapelUsuario
        {
            UsuarioId = usuarioId,
            PapelId = papelId
        });
    }

    public Task RemoverPapelAsync(int usuarioId, int papelId)
        => _papelUsuarioRepository.RemoverAsync(usuarioId, papelId);
}
