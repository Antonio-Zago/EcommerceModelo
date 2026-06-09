using Application.Dtos.Importacao;

namespace Application.Interfaces;

public interface IImportacaoProdutosService
{
    /// <summary>
    /// Importa uma lista de linhas já extraídas do Excel.
    /// <paramref name="webRootPath"/> é usado para resolver os caminhos físicos das imagens.
    /// </summary>
    Task<ResultadoImportacaoDto> ImportarAsync(
        IEnumerable<LinhaImportacaoDto> linhas,
        string webRootPath,
        IProgress<(int processados, int total)>? progresso = null);
}
